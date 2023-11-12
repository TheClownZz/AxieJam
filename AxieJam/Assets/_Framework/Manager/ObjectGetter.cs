using Skywatch.AssetManagement;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectGetter : AssetGetter
{
    public override void Load()
    {
        base.Load();
        AsyncOperationHandle<Object> loadHandle;
        if (AssetManager.TryGetOrLoadObjectAsync(assetReference, out loadHandle))
        {
            OnAssetLoaded(loadHandle);
        }
        else
        {
            loadHandle.Completed += op =>
            {
                OnAssetLoaded(loadHandle);
            };
        }
    }
    public virtual void OnAssetLoaded(AsyncOperationHandle<Object> handle)
    {
        SetLoad(true);
        OnComplete?.Invoke();
    }
}
