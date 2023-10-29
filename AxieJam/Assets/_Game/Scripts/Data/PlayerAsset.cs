using Spine.Unity;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

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
    Number,
    AtkSpeed,
    Range,
    Cooldown,
    ActiveValue,
    Size,
    ExtraNumber,
    ExtraDamage,
}
[System.Serializable]
public class SkillProperties
{
    public SkillType type;
    public float value;
}
[System.Serializable]

public class SkillDefaultProperties
{
    public int potionRequire = 15;
    public float duration = 3;
    public float cooldown = 20;
}

[System.Serializable]
public class SkillConfig
{
    public SkillDefaultProperties defaultValue;
    [TableList(ShowIndexLabels = true)]
    public List<SkillProperties> propertieList;
    public float GetSkillValue(SkillType type, float defaultValue = 0)
    {
        var propertie = propertieList.Find(x => x.type == type);

        if (propertie != null)
            return propertie.value;
        return defaultValue;
    }

}

[System.Serializable]
public class PlayerStatConfig
{
    public int foodRequire = 5;
    public float hp = 50;
    public float damage = 5;
    public float critRate = 0.2f;
}
[System.Serializable]
public class PlayerConfig
{
    public PlayerType type;

    public float hp = 200;
    public float damage = 10;
    public float moveSpeed = 5;
    public float critRate = 0.05f;
    public float critDamage = 2;
    public float armor = 0.5f;
  //  public float armor = 0;
    [TableList(ShowIndexLabels = true)]
    public List<PlayerStatConfig> levelConfiglist;
    [TableList(ShowIndexLabels = true)]
    public List<SkillConfig> skillConfiglist;
    public SkeletonDataAsset dataAsset;
    public PlayerStatConfig GetLevelConfig(int lv)
    {
        return levelConfiglist[Mathf.Min(lv - 1, levelConfiglist.Count - 1)];
    }

    public SkillConfig GetSkillConfig(int lv)
    {
        return skillConfiglist[Mathf.Min(lv - 1, skillConfiglist.Count - 1)];
    }

}


[CreateAssetMenu(menuName = "Game/PlayerAsset", fileName = "PlayerAsset")]
public class PlayerAsset : GameAsset
{
    public Sprite avatar;
    public Sprite skillIcon;

    public string axieName;
    public string skillName;
    public string discription;
    public Player prefab;
    public PlayerConfig data;

}
