using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelController : MonoBehaviour
{
    const int maxEnemy = 100;
    const float step = 0.1f;
    const float viewHorizonl = 5;
    const float viewVerticle = 7.5f;

    [SerializeField] float delaySpawn = 0.5f;
    [SerializeField] List<Enemy> enemyList;
    [SerializeField] List<float> timeSpawnList;
    public Transform top, bot, left, right;
    Transform player;
    WaitForSeconds delay;
    GameLevelAsset asset;
    ScreenGame screen;

    public WaveConfig config;

    Tween bossTween;
    float spawnRadius = 7.5f;

    private void Awake()
    {
        delay = new WaitForSeconds(step);
    }
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
    public void OnLevelLoad(int waveIndex)
    {
        this.waveIndex = waveIndex;
        screen = UIManager.Instance.GetScreen<ScreenGame>();
        config = asset.GetConfig(waveIndex);

    }

    int waveIndex;
    public void StartSpawn()
    {
        StartCoroutine(ISpawn());
        if (waveIndex == asset.dataList.Count)
        {
            SpawnBoss();
        }
    }
    public void OnLose()
    {
        StopAllCoroutines();
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
        timeSpawnList.Clear();
        float time = config.waveTime;
        WaveConfig waveConfig = config;
        List<EnemyWaveConfig> enemyConfigList = waveConfig.enemyConfigList;

        for (int i = 0; i < enemyConfigList.Count; i++)
        {
            timeSpawnList.Add(enemyConfigList[i].timeSpawn);
        }
        while (true)
        {
            yield return delay;
            time -= step;
            if (time <= 0)
                break;
            for (int i = 0; i < enemyConfigList.Count; i++)
            {
                timeSpawnList[i] -= step;
                if (timeSpawnList[i] <= 0)
                {
                    timeSpawnList[i] = enemyConfigList[i].timeSpawn;
                    for (int j = 0; j < enemyConfigList[i].numberSpawn; j++)
                    {
                        SpawnEnemy(enemyConfigList[i].type, waveConfig.waveProterties);
                    }
                }
            }
        }

        GameManager.Instance.OnCompleteLevel();
    }
    public Enemy SpawnEnemy(Transform pf)
    {
        Enemy e = PoolManager.Instance.SpawnObject(pf).GetComponent<Enemy>();
        enemyList.Add(e);
        return e;
    }
    public void SpawnEnemy(EnemyType enemyType, WaveStat stat)
    {
        if (enemyList.Count >= maxEnemy)
        {
            return;
        }

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
    private Vector3 GetOutSidePlayer(Vector3 pos)
    {
        float maxViewX = player.position.x + viewHorizonl;
        float minViewX = player.position.x - viewHorizonl;

        if (pos.x < maxViewX && pos.x > minViewX)
        {
            if (maxViewX > right.position.x)
            {
                pos.x = minViewX;
            }
            else
            {
                pos.x = maxViewX;
            }
        }

        float maxViewY = player.position.y + viewVerticle;
        float minViewY = player.position.y - viewVerticle;

        if (pos.y < maxViewY && pos.y > minViewY)
        {
            if (maxViewY > top.position.y)
            {
                pos.y = minViewY;
            }
            else
            {
                pos.y = maxViewY;
            }
        }
        return pos;
    }
    public List<Enemy> GetEnemyList()
    {
        return enemyList;
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
    }

    private void SpawnBoss()
    {
        bossTween = DOVirtual.DelayedCall(asset.bossSpawnConfig.delayTime, () =>
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
        });
    }

}
