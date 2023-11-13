using System.Collections.Generic;


public class GameAssetController : AssetController
{
    public override void UpdateAssetList(List<AssetGetter> getterList)
    {
        base.UpdateAssetList(getterList);
        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        var assetList = DataManager.Instance.GetAsset<PlayerListAsset>();

        foreach (var playerType in team)
        {
            var asset = assetList.GetAsset(playerType);
            getterList.Add(asset.prefabGetter);
            getterList.Add(asset.dataAssetGetter);
        }
    }
}
