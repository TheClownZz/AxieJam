using Sirenix.OdinInspector;
using Skywatch.AssetManagement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class AssetLoader
{
    public string sceneName;
    public AssetController controller;
    [SerializeField] protected List<AssetReference> assetReferenceList;

    protected List<AsyncOperationHandle> handleList;

    public AssetLoader()
    {
        assetReferenceList = new List<AssetReference>();
        handleList = new List<AsyncOperationHandle>();
    }

    public List<AssetReference> GetRefList()
    {
        return assetReferenceList;
    }

    public void LoadAsset(Action loadComplete = null)
    {
        Debug.LogError("Loading scene:" + sceneName);
        controller.UpdateAssetList(assetReferenceList);
        if (IsLoadAll())
        {
            loadComplete?.Invoke();
            return;
        }

        foreach (AssetReference assetRef in assetReferenceList)
        {
            AsyncOperationHandle<UnityEngine.Object> loadHandle;
            AssetManager.TryGetOrLoadObjectAsync(assetRef, out loadHandle);
            loadHandle.Completed += op =>
            {
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
                handleList.RemoveAt(i);
        }

        return handleList.Count == 0;
    }

    public void UnLoadAsset()
    {
        foreach (AssetReference assetRef in assetReferenceList)
        {
            AssetManager.Unload(assetRef);
        }
        handleList.Clear();
    }
}
public class AssetController : MonoBehaviour
{
    public virtual void UpdateAssetList(List<AssetReference> assetReferenceList)
    {

    }
}
