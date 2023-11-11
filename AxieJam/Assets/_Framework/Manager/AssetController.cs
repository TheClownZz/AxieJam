using Sirenix.OdinInspector;
using Skywatch.AssetManagement;
using System;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class AssetLoader
{
    public string sceneName;
    public AssetController controller;
    [SerializeField] protected List<AssetGetter> getterList;

    public AssetLoader()
    {
        getterList = new List<AssetGetter>();
    }

    public List<AssetGetter> GetRefList()
    {
        return getterList;
    }

    public void LoadAsset(Action loadComplete = null)
    {
        Debug.LogError("Loading scene:" + sceneName);
        controller.UpdateAssetList(getterList);
        if (IsLoadAll())
        {
            loadComplete?.Invoke();
            return;
        }

        foreach (AssetGetter getter in getterList)
        {
            AsyncOperationHandle<UnityEngine.Object> loadHandle;
            AssetManager.TryGetOrLoadObjectAsync(getter.assetReference, out loadHandle);
            loadHandle.Completed += op =>
            {
                getter.OnAssetLoaded(loadHandle);
                if (IsLoadAll()) loadComplete?.Invoke();
            };
        }
    }

    public bool IsLoadAll()
    {
       foreach(AssetGetter getter in getterList)
        {
            if (!AssetManager.IsLoaded(getter.assetReference)) return false;
        }
        return true;
    }

    public void UnLoadAsset()
    {
        foreach (AssetGetter getter in getterList)
        {
            AssetManager.Unload(getter.assetReference);
        }
    }
}
public class AssetController : MonoBehaviour
{
    public virtual void UpdateAssetList(List<AssetGetter> getterList)
    {

    }
}
