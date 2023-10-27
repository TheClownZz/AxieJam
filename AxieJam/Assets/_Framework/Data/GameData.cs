using System.Collections;
using UnityEngine;

interface IGameData
{
    void Initiate();
    void NewData();
    void UpdateData();
    void Delete();
    void SaveData();
    void LoadData();
    bool HasData();
    bool HasUpdateData();
    IEnumerator ProcessData();
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

    public virtual void UpdateData()
    {

    }

    public virtual void Delete()
    {
    }

    public virtual void SaveData()
    {

    }

    public virtual void LoadData()
    {

    }

    public virtual bool HasData()
    {
        return DataManager.Instance.HasData(GetName());
    }

    public virtual bool HasUpdateData()
    {
        return false;
    }

    public virtual IEnumerator ProcessData()
    {
        yield return null;
    }

    public virtual string GetName()
    {
        return GetType().FullName;
    }
}
