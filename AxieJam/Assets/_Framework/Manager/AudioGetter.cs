using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioGetter : AssetGetter
{
    [HideInInspector] public AudioClip clip;
    public override void OnAssetLoaded(AsyncOperationHandle<Object> handle)
    {
        base.OnAssetLoaded(handle);
        clip = (AudioClip)handle.Result;
    }
}
