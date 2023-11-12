using Skywatch.AssetManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetGetter : MonoBehaviour
{
    [HideInInspector] public bool isLoad;
    public AssetReference assetReference;
    public System.Action OnComplete;

    public void SetLoad(bool isLoad)
    {
        this.isLoad = isLoad;
    }
    public virtual void Load()
    {
        SetLoad(false);
    }

    public virtual void UnLoad()
    {
        SetLoad(false);
        OnComplete = null;
        AssetManager.Unload(assetReference);
    }
}
