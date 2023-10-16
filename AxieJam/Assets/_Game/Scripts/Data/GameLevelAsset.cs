using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Enemy_0 = 100,
    Enemy_1,
    Enemy_2,

    Boss_1 = 200,
}

[System.Serializable]
public class WaveStat
{
    public float hpRate = 1;
    public float damageRate = 1;
}

[System.Serializable]
public class EnemyWaveConfig
{
    public EnemyType type = EnemyType.Enemy_0;
    public float timeSpawn;
    public int numberSpawn;
}

[System.Serializable]

public class WaveConfig
{
    public float waveTime = 60;
    public WaveStat waveProterties;
    public List<EnemyWaveConfig> enemyConfigList;
}

 
[System.Serializable]
public class BossSpawnConfig
{
    public float delayTime;
    public WaveStat waveStat;

    public Transform pf;
}

[CreateAssetMenu(menuName = "Game/GameLevelAsset", fileName = "GameLevelAsset")]
public class GameLevelAsset : GameAsset
{
    [TableList(ShowIndexLabels = true)]
    public List<WaveConfig> dataList = new List<WaveConfig>();
    public BossSpawnConfig bossSpawnConfig;
    public WaveConfig GetConfig(int wave)
    {
        return dataList[wave];

    }

}
