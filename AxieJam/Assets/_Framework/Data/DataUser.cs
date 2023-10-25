using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataSavePlayer
{
    public PlayerType type;
    public int level;
    public int levelSkill;
    public int foodCount;
    public int potionCount;
    public DataSavePlayer(PlayerType type)
    {
        this.type = type;
        level = levelSkill = 1;
        potionCount = foodCount = 0;
    }
}

[System.Serializable]
public class DataSaveUser
{
    public List<DataSavePlayer> playerSaveList;
    public List<PlayerType> teamList;

    public void NewData()
    {
        playerSaveList = new List<DataSavePlayer>();
        for (int i = 0; i < (int)PlayerType.None; i++)
        {
            DataSavePlayer data = new DataSavePlayer((PlayerType)i);
            playerSaveList.Add(data);
        }

        teamList = new List<PlayerType>();
        for (int i = GameConfig.maxPlayer - 1; i >=0; i--)
        {
            teamList.Add((PlayerType)i);

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

    public DataSavePlayer GetDataPlayer(PlayerType type)
    {
        return dataSave.playerSaveList.Find(x => x.type == type);
    }

    public void UpdateFoodItem(PlayerType type, int number = 1)
    {
        DataSavePlayer player = GetDataPlayer(type);
        player.foodCount += number;
        SaveData();
    }

    public void UpdatePotionItem(PlayerType type, int number = 1)
    {
        DataSavePlayer player = GetDataPlayer(type);
        player.potionCount += number;
        SaveData();
    }
    public void UpLevel(PlayerType type, int itemCount)
    {
        DataSavePlayer player = GetDataPlayer(type);
        player.foodCount -= itemCount;
        player.level += 1;
        SaveData();
    }

    public void UpSkill(PlayerType type, int itemCount)
    {
        DataSavePlayer player = GetDataPlayer(type);
        player.potionCount -= itemCount;
        player.levelSkill += 1;
        SaveData();
    }

    public int GetLevel(PlayerType type)
    {
        DataSavePlayer player = GetDataPlayer(type);
        return player.level;
    }
    public int GetLevelSkill(PlayerType type)
    {
        DataSavePlayer player = GetDataPlayer(type);
        return player.levelSkill;
    }


    public List<PlayerType> GetTeam()
    {
        return dataSave.teamList;
    }

    public void SetTeam(List<PlayerType> list)
    {
        dataSave.teamList.Clear();
        dataSave.teamList.AddRange(list);
        SaveData();
    }
}
