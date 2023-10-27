using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
public class TeamAvtController : MonoBehaviour
{
    [SerializeField] List<SkeletonGraphic> skeletonList;
    [SerializeField] List<string> animList;

    public void UpdateTeam(List<PlayerType> team)
    {
        int random = Random.Range(0, animList.Count);
        PlayerListAsset playerListAsset = DataManager.Instance.GetAsset<PlayerListAsset>();

        for (int i = 0; i < skeletonList.Count; i++)
        {
            skeletonList[i].skeletonDataAsset = playerListAsset.GetAsset(team[i]).data.dataAsset;
            skeletonList[i].Initialize(true);
            skeletonList[i].AnimationState.SetAnimation(0, animList[(random + i) % skeletonList.Count], true);
        }

    }

    public void UpdateTeam(List<ItemSelect> itemSelectedList)
    {
        int random = Random.Range(0, animList.Count);
        PlayerListAsset playerListAsset = DataManager.Instance.GetAsset<PlayerListAsset>();

        for (int i = 0; i < skeletonList.Count; i++)
        {
            skeletonList[i].skeletonDataAsset = playerListAsset.GetAsset(itemSelectedList[i].playerType).data.dataAsset;
            skeletonList[i].Initialize(true);
            skeletonList[i].AnimationState.SetAnimation(0, animList[(random + i) % skeletonList.Count], true);
        }

    }
}
