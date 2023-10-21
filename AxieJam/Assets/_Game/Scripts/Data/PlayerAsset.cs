using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public enum PlayerType
{
    Blue,
    Orange,
    Red,
    Green,
    Purple,
    Pink,
    None,
}
public enum SkillType
{
    Damage,
    NumberBullet,
    AtkSpeed,
    Range,
    ActiveTime,
    ActiveValue,
    Size,
    ExtraBullet,
    ExtraDamage,
}
[System.Serializable]
public class PlayerSkillProperties
{
    public SkillType type;
    public float value;
}
[System.Serializable]

public class PlayerSkillDefaultProperties
{
    public int itemRequire = 15;
    public float duration = 3;
    public float cooldown = 20;
}

[System.Serializable]
public class PlayerSkillConfig
{
    public PlayerSkillDefaultProperties defaultValue;
    [TableList(ShowIndexLabels = true)]
    public List<PlayerSkillProperties> propertieList;
    public float GetSkillValue(SkillType type, float defaultValue = 0)
    {
        var propertie = propertieList.Find(x => x.type == type);

        if (propertie != null)
            return propertie.value;
        return defaultValue;
    }

}

[System.Serializable]
public class PlayerLevelConfig
{
    public int item = 5;
    public float hp = 50;
    public float damage = 5;
    public float attackSpeed = 0.5f;
    public float moveSpeed = 0.5f;
    public float critRate = 0.2f;
    public float critDamage = 0;
    public float armor = 0;
}
[System.Serializable]
public class PlayerConfig
{
    public PlayerType type;
    public Sprite avatar;

    public float hp = 200;
    public float damage = 10;
    public float attackSpeed = 1;
    public float moveSpeed = 5;
    public float critRate = 0.05f;
    public float critDamage = 2;
    public float armor = 0;
    [TableList(ShowIndexLabels = true)]
    public List<PlayerLevelConfig> levelConfiglist;
    [TableList(ShowIndexLabels = true)]
    public List<PlayerSkillConfig> skillConfiglist;

    public PlayerLevelConfig GetLevelConfig(int lvIndex)
    {
        return levelConfiglist[Mathf.Min(lvIndex, levelConfiglist.Count - 1)];
    }

    public PlayerSkillConfig GetSkillConfig(int lvIndex)
    {
        return skillConfiglist[Mathf.Min(lvIndex, levelConfiglist.Count - 1)];
    }

}


[CreateAssetMenu(menuName = "Game/PlayerAsset", fileName = "PlayerAsset")]
public class PlayerAsset : GameAsset
{
    public PlayerConfig data;
    public Player prefab;
}
