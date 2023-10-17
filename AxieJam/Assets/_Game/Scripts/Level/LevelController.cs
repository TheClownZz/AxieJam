using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelController : MonoBehaviour
{
    const float step = 0.1f;
    const float viewHorizonl = 5;
    const float viewVerticle = 7.5f;

    public Transform top, bot, left, right;

    int waveIndex;
    Tween bossTween;
    Transform player;
    GameLevelAsset asset;
    WaveConfig waveConfig;
    Coroutine spawnCoroutine;

    float spawnRadius = 7.5f;
    float delaySpawn = 0.5f;
    [SerializeField] List<Enemy> enemyList;

    public void OnUpdate(float dt)
    {
        foreach (var enemy in enemyList)
            enemy.OnUpdate(dt);
    }
    public void OnInits()
    {
        player = GameManager.Instance.player.transform;
        spawnRadius = GameManager.Instance.gameConfig.spawnRadius;
    }
    public void SetAsset(GameLevelAsset asset)
    {
        this.asset = asset;
    }
    public void LoadLevel()
    {
        waveIndex = 0;
        waveConfig = asset.GetConfig(waveIndex);
        spawnCoroutine = StartCoroutine(ISpawn());
    }

    private void LoadNextWave()
    {
        waveIndex += 1;
        if (waveIndex == asset.dataList.Count)
        {
            SpawnBoss();
        }
        else
        {
            spawnCoroutine = StartCoroutine(ISpawn());
        }
    }

    public void OnLose()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null; ;
        }
        foreach (var e in enemyList)
            e.OnLose();
    }

    public void DestroyCurrentLevel()
    {
        StopAllCoroutines();
        foreach (var enemy in enemyList)
            enemy.Clear();
        enemyList.Clear();
        if (bossTween != null)
            bossTween.Kill();
    }
    public IEnumerator ISpawn()
    {
        float time = waveConfig.waveTime;
        List<EnemyWaveConfig> enemyConfigList = waveConfig.enemyConfigList;

        for (int i = 0; i < enemyConfigList.Count; i++)
        {
            for (int j = 0; j < enemyConfigList[i].numberSpawn; j++)
            {
                SpawnEnemy(enemyConfigList[i].type, waveConfig.waveProterties);
            }
        }
        yield return new WaitForSeconds(time);
        LoadNextWave();
    }
    public Enemy SpawnEnemy(Transform pf)
    {
        Enemy e = PoolManager.Instance.SpawnObject(pf).GetComponent<Enemy>();
        enemyList.Add(e);
        return e;
    }
    public void SpawnEnemy(EnemyType enemyType, WaveStat stat)
    {
        Vector2 randomPos = Quaternion.Euler(0f, 0f, Random.Range(0, 360)) * Vector2.right * spawnRadius;
        Vector3 pos = player.position + (Vector3)randomPos;
        pos.x = Mathf.Clamp(pos.x, left.position.x, right.position.x);
        pos.y = Mathf.Clamp(pos.y, bot.position.y, top.position.y);

        Enemy e = PoolManager.Instance.SpawnObject((PoolType)enemyType).GetComponent<Enemy>();

        e.SetStat();
        e.SetWaveStat(stat);
        e.DelaySpawn(delaySpawn, pos);
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
    private void SpawnBoss()
    {
        Vector2 randomPos = Quaternion.Euler(0f, 0f, Random.Range(0, 360)) * Vector2.right * spawnRadius;
        Vector3 pos = player.position + (Vector3)randomPos;
        pos.x = Mathf.Clamp(pos.x, left.position.x, right.position.x);
        pos.y = Mathf.Clamp(pos.y, bot.position.y, top.position.y);

        Enemy e = PoolManager.Instance.SpawnObject(asset.bossSpawnConfig.pf).GetComponent<Enemy>();

        e.SetStat();
        e.SetWaveStat(asset.bossSpawnConfig.waveStat);
        e.OnInit();
        e.DelaySpawn(1, pos);
        enemyList.Add(e);
    }
    public List<Enemy> GetEnemyList()
    {
        return enemyList;
    }

}
