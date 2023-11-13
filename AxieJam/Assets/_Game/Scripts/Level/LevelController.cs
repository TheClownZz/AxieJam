using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelController : MonoBehaviour
{

    public Transform top, bot, left, right;

    [SerializeField] int waveIndex;
    Tween bossTween;
    LevelAsset asset;
    WaveConfig waveConfig;

    float delaySpawn = 1f;
    [HideInInspector] public List<Enemy> enemyList;


    public void OnUpdate(float dt)
    {
        int enemyCount = enemyList.Count;
        for (int i = 0; i < enemyCount; i++)
        {
            enemyList[i].OnUpdate(dt);
        }
    }

    public void SetAsset(LevelAsset asset)
    {
        this.asset = asset;
    }
    public void LoadLevel()
    {
        waveIndex = 0;
        waveConfig = asset.GetConfig(waveIndex);
        SpawnWave();
        UIManager.Instance.GetScreen<ScreenGame>().UpdateWave(waveIndex + 1, asset.dataList.Count);

    }

    private void LoadNextWave()
    {
        waveIndex += 1;

        if (waveIndex < asset.dataList.Count)
        {
            waveConfig = asset.GetConfig(waveIndex);
            SpawnWave();
            UIManager.Instance.GetScreen<ScreenGame>().UpdateWave(waveIndex + 1, asset.dataList.Count);
        }
        else
        {
            GameManager.Instance.OnWinMap();
        }
    }

    public void OnLose()
    {

        foreach (var e in enemyList)
            e.OnLose();
    }

    public void ClearCurrentLevel()
    {
        StopAllCoroutines();
        foreach (var enemy in enemyList)
            enemy.Clear();
        enemyList.Clear();
        if (bossTween != null)
            bossTween.Kill();
    }
    public void SpawnWave()
    {
        List<EnemyWaveConfig> enemyConfigList = waveConfig.enemyConfigList;

        for (int i = 0; i < enemyConfigList.Count; i++)
        {
            for (int j = 0; j < enemyConfigList[i].numberSpawn; j++)
            {
                SpawnEnemy(enemyConfigList[i].type, waveConfig.waveProterties);
            }
        }

    }

    public void SpawnEnemy(EnemyType enemyType, WaveStat stat)
    {
        Enemy e;
        if (enemyType >= EnemyType.Boss_1)
        {
            Transform prefab = asset.GetBossAsset(enemyType).prefabGetter.prefab.transform;
            e = PoolManager.Instance.SpawnObject(prefab).GetComponent<Enemy>();
            e.GetComponent<SetupBossData>().asset = asset.GetBossAsset(enemyType).asset;
        }
        else
        {
           
            Transform prefab = asset.GetEnemyAsset(enemyType).prefabGetter.prefab.transform;
            e = PoolManager.Instance.SpawnObject(prefab).GetComponent<Enemy>();
            e.GetComponent<SetupEnemyData>().asset = asset.GetEnemyAsset(enemyType).asset;

        }
        e.transform.SetParent(transform);
        e.SetStat();
        e.SetWaveStat(stat);
        e.DelaySpawn(delaySpawn, GetSpawnErea());
        enemyList.Add(e);
    }
    public void RemoveEnemy(Enemy enemy)
    {
        int index = enemyList.FindIndex(x => x == enemy);
        if (index != -1)
        {
            Enemy e = enemyList[enemyList.Count - 1];
            enemyList[enemyList.Count - 1] = enemyList[index];
            enemyList[index] = e;

            enemyList.RemoveAt(enemyList.Count - 1);
        }

        if (enemyList.Count == 0)
        {
            LoadNextWave();
        }
    }

    public Vector3 GetSpawnErea()
    {
        Vector2 pos = Random.insideUnitCircle * GameManager.Instance.gameConfig.spawnRadius;
        pos.x = Mathf.Clamp(pos.x, left.position.x, right.position.x);
        pos.y = Mathf.Clamp(pos.y, bot.position.y, top.position.y - 2f);
        return pos;
    }
    public Vector3 GetSpawnErea(Vector3 pos)
    {
        pos.x = Mathf.Clamp(pos.x, left.position.x, right.position.x);
        pos.y = Mathf.Clamp(pos.y, bot.position.y, top.position.y - 2f);
        return pos;
    }
}
