using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Axe,
    Baseketball,
    Bazooka,
    EnemyGun,
    GaltingGun,
    Katana,
    Knife,
    M416,
    Pan,
    Pisol,
    Rock,
    ShortBow,
    LongBow,
    ShotGun,
}

[System.Serializable]
public class WeaponAsset
{
    public WeaponType WeaponType;
    public Weapon pf;
    public WeaponConfig config;
}
[CreateAssetMenu(menuName = "Game/WeaponListAsset", fileName = "WeaponListAsset")]
public class WeaponListAsset : GameAsset
{
    public List<WeaponAsset> dataList;

    public WeaponAsset GetAsset(WeaponType weaponType)
    {
        return dataList.Find(x => x.WeaponType == weaponType);
    }

    [Button]
    public void Helper()
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            // dataList[i].WeaponType = (WeaponType)(i + 1);
            dataList[i].pf.config = dataList[i].config;
        }
    }
}
