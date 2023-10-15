using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RarityConfig
{
    public WeaponRarity weaponRarity;
    public List<WeaponAsset> weaponList;
}

[CreateAssetMenu(menuName = "Game/RarityAsset", fileName = "RarityAsset")]
public class RarityAsset : GameAsset
{
    public List<RarityConfig> dataList;

    [SerializeField] WeaponListAsset asset;
    [Button]
    public void Helper()
    {
        foreach(var wpData in asset.dataList)
        {
            var data = dataList.Find(x => x.weaponRarity == wpData.config.rarity);
            if (data != null)
            {
                data.weaponList.Add(wpData);
            }
        }
    }
}
