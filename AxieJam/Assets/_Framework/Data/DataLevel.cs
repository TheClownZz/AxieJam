using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevel : GameData
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    [SerializeField]
    private LevelSave levelSave;
    #endregion

    #region PARAMS    
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS

    public LevelSave LevelSave { get => levelSave; set => levelSave = value; }
    public int CurrentLevelId { get => this.levelSave.currentLevel; set => this.levelSave.currentLevel = value; }
    public int HighestLevelId { get => this.levelSave.highestLevel; set => this.levelSave.highestLevel = value; }
    #endregion

    #region METHODS

    public int GetCurrentLevel()
    {
        return this.LevelSave.currentLevel;
    }

    public override void SaveData()
    {
        DataManager.Instance.SaveData<LevelSave>(GetType().FullName, LevelSave);
    }

    public override void LoadData()
    {
        levelSave = DataManager.Instance.LoadData<LevelSave>(GetType().FullName);
    }

    public override void NewData()
    {
        levelSave = new LevelSave(1, 0);
        SaveData();
    }

    public void PassLevel()
    {
        if (CurrentLevelId > HighestLevelId)
            HighestLevelId = CurrentLevelId;
        CurrentLevelId++;
        SaveData();
    }

    #endregion

    #region DEBUG
    #endregion
}