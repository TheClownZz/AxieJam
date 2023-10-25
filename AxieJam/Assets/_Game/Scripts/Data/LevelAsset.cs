using JetBrains.Annotations;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Enemy_A = 100,
    Enemy_B,
    Enemy_C,

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
    public EnemyType type = EnemyType.Enemy_A;
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
public class EnemyAssetType
{
    public EnemyAsset asset;
    public EnemyType enemyType;
}
[System.Serializable]
public class BossAssetType
{
    public BossAsset asset;
    public EnemyType enemyType;
}

[CreateAssetMenu(menuName = "Game/LevelAsset", fileName = "LevelAsset")]
public class LevelAsset : GameAsset
{
    [TableList(ShowIndexLabels = true)]
    public List<WaveConfig> dataList = new List<WaveConfig>();
    public List<EnemyAssetType> enemyDataSetupList;
    public List<BossAssetType> bossDataSetupList;
    public WaveConfig GetConfig(int wave)
    {
        return dataList[wave];
    }

    public BossAsset GetBossAsset(EnemyType enemyType)
    {
        return bossDataSetupList.Find(x => x.enemyType == enemyType).asset;
    }

    public EnemyAsset GetEnemyAsset(EnemyType enemyType)
    {
        return enemyDataSetupList.Find(x => x.enemyType == enemyType).asset;
    }
}
