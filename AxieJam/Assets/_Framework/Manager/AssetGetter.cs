using Skywatch.AssetManagement;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetGetter : MonoBehaviour
{
    [SerializeField] AssetReference assetReference;
    public Action<UnityEngine.Object> OnGetAsset;
    public void LoadAsset()
    {
        if (AssetManager.TryGetOrLoadObjectAsync(assetReference, out AsyncOperationHandle<UnityEngine.Object> handle))
        {
            OnGetAsset?.Invoke(handle.Result);
        }
        else
        {
            handle.Completed += op =>
            {
                OnGetAsset?.Invoke(handle.Result);
            };
        }
    }
}
