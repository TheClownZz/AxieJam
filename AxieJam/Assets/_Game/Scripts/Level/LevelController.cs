using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelController : MonoBehaviour
{

    public Transform top, bot, left, right;

    int waveIndex;
    Tween bossTween;
    LevelAsset asset;
    WaveConfig waveConfig;

    float spawnRadius = 7.5f;
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
    public void OnInits()
    {
        spawnRadius = GameManager.Instance.gameConfig.spawnRadius;
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
        // float time = waveConfig.waveTime;
        List<EnemyWaveConfig> enemyConfigList = waveConfig.enemyConfigList;

        for (int i = 0; i < enemyConfigList.Count; i++)
        {
            for (int j = 0; j < enemyConfigList[i].numberSpawn; j++)
            {
                SpawnEnemy(enemyConfigList[i].type, waveConfig.waveProterties);
            }
        }

    }
    public Enemy SpawnEnemy(Transform pf)
    {
        Enemy e = PoolManager.Instance.SpawnObject(pf).GetComponent<Enemy>();
        enemyList.Add(e);
        return e;
    }
    public void SpawnEnemy(EnemyType enemyType, WaveStat stat)
    {
        Enemy e = PoolManager.Instance.SpawnObject((PoolType)enemyType).GetComponent<Enemy>();
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
        Vector2 pos = Quaternion.Euler(0f, 0f, Random.Range(0, 360)) * Vector2.right * spawnRadius;
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
