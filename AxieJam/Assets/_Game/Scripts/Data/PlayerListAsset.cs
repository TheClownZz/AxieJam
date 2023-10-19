using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 

[CreateAssetMenu(menuName = "Game/PlayerListAsset", fileName = "PlayerListAsset")]
public class PlayerListAsset : GameAsset
{
    public List<PlayerAsset> listAsset;
    public PlayerAsset GetAsset(PlayerType type)
    {
        return listAsset.Find( x => x.data.type == type);
    }
}
