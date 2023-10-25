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

 

[CreateAssetMenu(menuName = "Game/GameLevelAsset", fileName = "GameLevelAsset")]
public class GameLevelAsset : GameAsset
{
    [TableList(ShowIndexLabels = true)]
    public List<WaveConfig> dataList = new List<WaveConfig>();
    public WaveConfig GetConfig(int wave)
    {
        return dataList[wave];

    }

}
