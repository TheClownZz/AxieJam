using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AssetLoader
{
    public string sceneName;
    public AssetController controller;
    [SerializeField] protected List<AssetGetter> getterList;

    public void LoadAsset()
    {
        Debug.LogError("Loading scene:" + sceneName);
        controller.UpdateAssetList(getterList);
        foreach (AssetGetter getter in getterList)
        {
            getter.Load();
        }
    }

    public bool IsLoadAll()
    {
        foreach (AssetGetter getter in getterList)
        {

            if (!getter.isLoad)
            {
                return false;
            }
        }
        return true;
    }

    public void UnLoadAsset(AssetLoader currentLoader)
    {
        foreach (AssetGetter getter in getterList)
        {
            if (!currentLoader.getterList.Contains(getter))
            {
                getter.UnLoad();

            }
        }
    }
}
public class AssetController : MonoBehaviour
{
    public virtual void UpdateAssetList(List<AssetGetter> getterList)
    {

    }
}
