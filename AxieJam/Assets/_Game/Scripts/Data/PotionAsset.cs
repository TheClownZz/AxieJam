using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PotionConfig
{
    public PlayerType type;
    public Sprite sprite;
    public Sprite uiSprite;

}
[CreateAssetMenu(menuName = "Game/PotionAsset", fileName = "PotionAsset")]

public class PotionAsset : GameAsset
{
    public List<PotionConfig> dataList;
    public PotionConfig GetConfig(PlayerType type)
    {
        return dataList.Find(x => x.type == type);
    }
}
