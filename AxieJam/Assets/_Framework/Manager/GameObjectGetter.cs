using Skywatch.AssetManagement;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameObjectGetter : AssetGetter
{
    public GameObject prefab;
    public override void Load()
    {
        base.Load();
        AsyncOperationHandle<GameObject> loadHandle;
        if (AssetManager.TryInstantiateOrLoadAsync(assetReference, Vector3.zero, Quaternion.identity, SceneController.Instance.transform, out loadHandle))
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

    public virtual void OnAssetLoaded(AsyncOperationHandle<GameObject> handle)
    {
        SetLoad(true);
        OnComplete?.Invoke();
        prefab = handle.Result;
        prefab.SetActive(false);
    }

    public override void UnLoad()
    {
        AssetManager.DestroyAllInstances(assetReference);
        base.UnLoad();
    }
}
