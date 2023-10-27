using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossSkillConfig
{
    public List<SkillProperties> skillPropertieList;
    public float GetSkillValue(SkillType skillType, float defaultValue = 0)
    {
        SkillProperties properti = skillPropertieList.Find(x=>x.type == skillType);
        if(properti != null )
            return properti.value;
        return defaultValue;
    }

}
[System.Serializable]
public class BossConfig : EnemyConfig
{
    public List<BossSkillConfig> skilldDataList;

}
[CreateAssetMenu(menuName = "Game/BossAsset", fileName = "BossAsset")]

public class BossAsset : GameAsset
{
    public BossConfig data;

}
