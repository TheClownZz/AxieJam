using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelSaveData
{
    [SerializeField]
    private int id;
    [SerializeField]
    private int enemyLeft;

    public int Id { get => id; set => id = value; }
    public int EnemyLeft { get => enemyLeft; set => enemyLeft = value; }
}

[System.Serializable]
public class LevelSave
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    #endregion

    #region PARAMS
    public int currentLevel;
    public int highestLevel;
    public List<LevelSaveData> dataList = new List<LevelSaveData>();
    #endregion

    #region PROPERTIES
    public LevelSave(int currentLevelId, int highestLevelId)
    {
        this.currentLevel = currentLevelId;
        this.highestLevel = highestLevelId;
    }
    #endregion

    #region EVENTS
    #endregion

    #region METHODS
    #endregion

    #region DEBUG
    #endregion
}

