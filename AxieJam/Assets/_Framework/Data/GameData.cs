using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IGameData
{
    void Initiate();
    void NewData();
    void SaveData();
    void LoadData();
    bool HasData();
    string GetName();
}

public class GameData : MonoBehaviour, IGameData
{
    public virtual void Initiate()
    {

    }

    public virtual void NewData()
    {

    }


    public virtual void SaveData()
    {

    }

    public virtual void LoadData()
    {

    }

    public bool HasData()
    {
        return DataManager.Instance.HasData(GetName());
    }


    public string GetName()
    {
        return GetType().FullName;
    }
}
