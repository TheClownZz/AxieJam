using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
public class AudioGetter : ObjectGetter
{
    public AudioClip clip;
    public override void OnAssetLoaded(AsyncOperationHandle<Object> handle)
    {
        clip = (AudioClip)handle.Result;
        base.OnAssetLoaded(handle);
    }
}
