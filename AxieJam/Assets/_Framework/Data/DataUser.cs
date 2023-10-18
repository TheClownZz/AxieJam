using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Blue,
    Orange,
    Red,
    Green,
    Purple,
    Pink,
    None,
}

[System.Serializable]
public class DataSavePlayer
{
    public PlayerType type;
    public int level;
    public int levelSkill;
    public int itemLevelCount;
    public int itemSkillCount;

    public DataSavePlayer(PlayerType type)
    {
        this.type = type;
        level = levelSkill = 1;
        itemSkillCount = itemLevelCount = 0;
    }
}

[System.Serializable]
public class DataSaveUser
{
    public List<DataSavePlayer> playerSaveList;

    public void NewData()
    {
        playerSaveList = new List<DataSavePlayer>();
        for (int i = 0; i < (int)PlayerType.None; i++)
        {
            DataSavePlayer data = new DataSavePlayer((PlayerType)i);
            playerSaveList.Add(data);
        }
    }


}

public class DataUser : GameData
{


    [SerializeField] private DataSaveUser dataSave;

    public DataSaveUser DataSave { get => dataSave; set => dataSave = value; }

    public override void SaveData()
    {
        DataManager.Instance.SaveData<DataSaveUser>(GetName(), dataSave);
    }

    public override void LoadData()
    {
        dataSave = DataManager.Instance.LoadData<DataSaveUser>(GetName());
    }

    public override void NewData()
    {
        dataSave = new DataSaveUser();
        dataSave.NewData();
    }

    private DataSavePlayer GetPlayer(PlayerType type)
    {
        return dataSave.playerSaveList.Find(x => x.type == type);
    }

    public void UpdateLevellItem(PlayerType type, int number = 1)
    {
        DataSavePlayer player = GetPlayer(type);
        player.itemLevelCount += number;
        SaveData();
    }

    public void UpdateSkillItem(PlayerType type, int number = 1)
    {
        DataSavePlayer player = GetPlayer(type);
        player.itemSkillCount += number;
        SaveData();
    }
    public void UpLevel(PlayerType type, int itemCount)
    {
        DataSavePlayer player = GetPlayer(type);
        player.itemLevelCount -= itemCount;
        player.level += 1;
        SaveData();
    }

    public void UpSkill(PlayerType type, int itemCount)
    {
        DataSavePlayer player = GetPlayer(type);
        player.itemSkillCount -= itemCount;
        player.levelSkill += 1;
        SaveData();
    }

    public int GetLevel(PlayerType type)
    {
        DataSavePlayer player = GetPlayer(type);
        return player.level;
    }

}
