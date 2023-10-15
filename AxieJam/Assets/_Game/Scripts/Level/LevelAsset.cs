using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/LevelAsset", fileName = "LevelAsset")]
public class LevelAsset : ScriptableObject
{
    public List<Level> levelsList = new List<Level>();

    public Level GetLevel(int levelIndex)
    {
        return levelsList[(levelIndex - 1) % levelsList.Count];
    }


    #region DEBUG

#if UNITY_EDITOR
    // private void OnValidate()
    // {
    //     bonusIndexList.Clear();
    //     normalIndexList.Clear();
    //     foreach (var item in levelsList)
    //     {
    //         if (item.isBonusLevel)
    //         {
    //             bonusIndexList.Add(levelsList.IndexOf(item) + 1);
    //         }
    //         else
    //         {
    //             normalIndexList.Add(levelsList.IndexOf(item) + 1);
    //         }
    //     }
    // }
#endif

    #endregion
}
