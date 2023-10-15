using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevel : GameData
{


    #region EDITOR PARAMS
    [SerializeField]
    private LevelSave levelSave;
    #endregion

    public LevelSave LevelSave { get => levelSave; set => levelSave = value; }
    public int CurrentLevelId { get => this.levelSave.currentLevel; set => this.levelSave.currentLevel = value; }
    public int HighestLevelId { get => this.levelSave.highestLevel; set => this.levelSave.highestLevel = value; }
    public int CurrentDay { get => this.levelSave.currentDay; set => this.levelSave.currentDay = value; }

    #region METHODS


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
        levelSave = new LevelSave(1, 0, 1);
        SaveData();
    }

    public void PassLevel()
    {
        CurrentDay = 1;
        if (CurrentLevelId > HighestLevelId)
            HighestLevelId = CurrentLevelId;
        CurrentLevelId++;
        SaveData();
    }

    public void PassDay()
    {
        CurrentDay += 1;
        SaveData();
    }

    public void ResetDay()
    {
        CurrentDay = 1;
        SaveData();
    }

    #endregion

    #region DEBUG
    #endregion
}