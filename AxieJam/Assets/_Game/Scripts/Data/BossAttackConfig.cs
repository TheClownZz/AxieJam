using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossDataType
{
    damage,
    range,
    coolDown,
}
[System.Serializable]
public class BossData
{
    public BossDataType type;
    public float value;

}


[CreateAssetMenu(menuName = "Game/BossSkillConfig", fileName = "BossSkillConfig")]
public class BossAttackConfig : ScriptableObject
{
    public List<BossData> dataList;
    public float GetValue(BossDataType type, float defaultValue = 0)
    {
        var data = dataList.Find(x => x.type == type);
        if (data != null)
            return data.value;
        return defaultValue;
    }
}
