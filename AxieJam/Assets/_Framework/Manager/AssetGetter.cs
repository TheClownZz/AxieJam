using Skywatch.AssetManagement;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetGetter : MonoBehaviour
{
    public AssetReference assetReference;
   
    public virtual void OnAssetLoaded(AsyncOperationHandle<UnityEngine.Object> handle)
    {
    }
}
