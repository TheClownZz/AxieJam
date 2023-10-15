using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal = 100,
    Tank,
    Run,
    Vomit,
    Twin,

    Porky = 200,
    Medic,
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
    public EnemyType type = EnemyType.Normal;
    public float timeSpawn;
    public int numberSpawn;
}

[System.Serializable]

public class WaveConfig
{
    public WaveStat waveProterties;
    public List<EnemyWaveConfig> enemyConfigList;
}

[System.Serializable]
public class DayProperties
{
    public int dayIndex;
    public float timeSurvival;
    public List<RarityDropConfig> dropList;

}

[System.Serializable]
public class RarityDropConfig
{
    public WeaponRarity weaponRarity;
    public float dropRate;
}


[System.Serializable]
public class DayConfig
{
    public DayProperties dayProperties;
    [TableList(ShowIndexLabels = true)]
    public WaveConfig waveConfig;

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
    public List<DayConfig> dataList = new List<DayConfig>();
    public BossSpawnConfig bossSpawnConfig;
    public WeaponType weaponStart;
    public DayConfig GetConfig(int day)
    {
        return dataList.Find(x => x.dayProperties.dayIndex == day);

    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            dataList[i].dayProperties.dayIndex = i + 1;
        }
    }
#endif
}
