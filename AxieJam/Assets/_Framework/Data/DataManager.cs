using System;
using UnityEngine;
using CI.QuickSave;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DataManager : MonoSingleton<DataManager>
{

    public List<GameAsset> assetsList = new List<GameAsset>();
    public List<GameData> gameDatasList = new List<GameData>();

    QuickSaveReader reader;
    QuickSaveWriter writer;
    const string rootKey = "dataRoot";

    private void SetupController()
    {
        writer = QuickSaveWriter.Create(rootKey);
        reader = QuickSaveReader.Create(rootKey);
    }

    protected override void Initiate()
    {
        base.Initiate();
        SetupController();
        foreach (GameData data in gameDatasList)
        {
            Debug.LogError(data.HasData());
            data.Initiate();
            if (!data.HasData())
            {
                data.NewData();
            }
            else
            {
                data.LoadData();
            }
        }
    }


    public T GetAsset<T>() where T : ScriptableObject
    {
        try
        {
            return assetsList.Find(x => x.GetType().FullName == typeof(T).FullName) as T;
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Missing ScriptableObject: {0}", typeof(T).FullName);
            return null;
        }
    }

    public T GetData<T>() where T : GameData
    {
        try
        {
            return gameDatasList.Find(x => x.GetName() == typeof(T).FullName) as T;
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Missing GameData: {0}", typeof(T).FullName);
            return null;
        }
    }

    public void SaveData<T>(string key, T userSaveData)
    {
        writer.Write(key, userSaveData).Commit();
    }

    public T LoadData<T>(string key)
    {
        return reader.Read<T>(key);
    }

    public bool HasData(string key)
    {
        return writer.Exists(key);
    }


    [Button]
    public void LoadAllData()
    {
        foreach (GameData data in gameDatasList)
        {
            if (data.HasData())
                data.LoadData();
            else
                data.NewData();
        }
    }

    [Button]
    public void SaveAllData()
    {
        foreach (GameData data in gameDatasList)
        {
            data.SaveData();
        }
    }

    [Button]
    public void DeleteAllData()
    {
        foreach (GameData data in gameDatasList)
        {
            writer.Delete(data.GetName());
        }
        writer.Commit();
        reader = QuickSaveReader.Create(rootKey);// syns with writer
    }

    private void OnApplicationQuit()
    {
        SaveAllData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveAllData();
        }
    }

    #region HELPERS

#if UNITY_EDITOR
    private void OnValidate()
    {
        assetsList.Clear();
        assetsList.AddRange(Resources.FindObjectsOfTypeAll<GameAsset>());

        gameDatasList.Clear();
        GetComponents<GameData>(gameDatasList);
    }
#endif

    #endregion

}
