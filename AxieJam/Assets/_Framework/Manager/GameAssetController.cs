using System.Collections.Generic;
using UnityEngine;

public class GameAssetController : AssetController
{
    [SerializeField] List<AssetGetter> assetGetterList;
    public override void UpdateAssetList(List<AssetGetter> getterList)
    {
        getterList.Clear();
        getterList.AddRange(assetGetterList);
        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        var assetList = DataManager.Instance.GetAsset<PlayerListAsset>();

        foreach (var playerType in team)
        {
            var playerAsset = assetList.GetAsset(playerType);
            getterList.Add(playerAsset.prefabGetter);
            getterList.Add(playerAsset.dataAssetGetter);
        }

        LevelAsset levelAsset = DataManager.Instance.GetData<DataLevel>().GetCurrentLevelAsset();
        foreach(var setup in levelAsset.enemyDataSetupList)
        {
            getterList.Add(setup.prefabGetter);
        }

        foreach (var setup in levelAsset.bossDataSetupList)
        {
            getterList.Add(setup.prefabGetter);
        }
    }
}
