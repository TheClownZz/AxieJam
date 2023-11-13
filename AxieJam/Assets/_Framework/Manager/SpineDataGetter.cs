using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpineDataGetter : ObjectGetter
{
    public SkeletonDataAsset dataAsset;
    public override void OnAssetLoaded(AsyncOperationHandle<Object> handle)
    {
        base.OnAssetLoaded(handle);
        dataAsset = (SkeletonDataAsset)handle.Result;
    }
}
