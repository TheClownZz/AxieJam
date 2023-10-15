using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponSpiecialDataType
{
    Rate,
}
public enum WeaponRarity
{
    Common,
    Rare,
    Special,
    Epic,
    Legend,

    None = 100
}

[System.Serializable]
public class WeaponStat
{
    public float damage;
    public float range;
    public float attackSpeed;
    public float knockBack = 1;

}

[System.Serializable]
public class WeaponSpecialData
{
    public WeaponSpiecialDataType type;
    public float value;
}

[CreateAssetMenu(menuName = "Game/WeaponAsset", fileName = "WeaponAsset")]
public class WeaponConfig : ScriptableObject
{
    public WeaponRarity rarity;
    public string weaponName;
    public Sprite icon;
    [TableList(ShowIndexLabels = true)]
    public List<WeaponStat> dataList;

    public List<WeaponSpecialData> dataSpecialList;
    public WeaponStat GetStat(int tier)
    {
        return dataList[tier];
    }

    public float GetValue(WeaponSpiecialDataType type, float defaultValue = 0)
    {
        WeaponSpecialData data = dataSpecialList.Find(x => x.type == type);
        if (data != null)
            return data.value;
        return defaultValue;
    }
}
