using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpine : MonoBehaviour
{
    [SerializeField] SkeletonAnimation anim;
    [SerializeField] List<SkeletonDataAsset> assetList;
    private void OnEnable()
    {
        anim.skeletonDataAsset = assetList[Random.Range(0, assetList.Count)];
        anim.Initialize(true);
    }
}
