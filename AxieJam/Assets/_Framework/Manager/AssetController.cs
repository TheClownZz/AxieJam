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

    protected List<AsyncOperationHandle<UnityEngine.Object>> handleList;

    public AssetLoader()
    {
        getterList = new List<AssetGetter>();
        handleList = new List<AsyncOperationHandle<UnityEngine.Object>>();
    }

    public List<AssetGetter> GetRefList()
    {
        return getterList;
    }

    public void LoadAsset(Action loadComplete = null)
    {
        Debug.LogError("Loading scene:" + sceneName);
        controller.UpdateAssetList(getterList);
        if (getterList.Count == 0)
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
            handleList.Add(loadHandle);
        }
    }

    public bool IsLoadAll()
    {
        for (int i = handleList.Count - 1; i >= 0; i--)
        {
            if (handleList[i].IsDone)
            {
                Debug.LogError("load done");
                getterList[i].OnAssetLoaded(handleList[i]);
                handleList.RemoveAt(i);
            }
        }

        return handleList.Count == 0;
    }

    public void UnLoadAsset()
    {
        foreach (AssetGetter getter in getterList)
        {
            AssetManager.Unload(getter.assetReference);
        }
        handleList.Clear();
    }
}
public class AssetController : MonoBehaviour
{
    public virtual void UpdateAssetList(List<AssetGetter> getterList)
    {

    }
}
