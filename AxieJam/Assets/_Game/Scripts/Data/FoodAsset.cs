using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class FoodConfig
{
    public PlayerType type;
    public Sprite sprite;
}

[CreateAssetMenu(menuName = "Game/FoodAsset", fileName = "FoodAsset")]
public class FoodAsset : GameAsset
{
    public List<FoodConfig> dataList;
    public FoodConfig GetConfig(PlayerType type)
    {
        return dataList.Find(x => x.type == type);
    }

}
