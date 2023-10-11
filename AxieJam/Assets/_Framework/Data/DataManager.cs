using System;
using System.Collections;
using System.Collections.Generic;
using CI.QuickSave;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


public class DataManager : MonoSingleton<DataManager>
{

    public List<GameAsset> assetsList = new List<GameAsset>();
    public List<GameData> gameDatasList = new List<GameData>();

    QuickSaveReader reader;
    QuickSaveWriter writer;
    const string rootKey = "dataRoot";

    private void SetupController()
    {
        reader = QuickSaveReader.Create(rootKey);
        writer = QuickSaveWriter.Create(rootKey);
    }

    protected override void Initiate()
    {
        base.Initiate();
        SetupController();
        for (int i = 0; i < gameDatasList.Count; i++)
        {
            var data = gameDatasList[i];
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
       // BayatGames.SaveGamePro.SaveGame.Save<T>(key, userSaveData);
    }

    public T LoadData<T>(string key)
    {
        return reader.Read<T>(key);
        //  return BayatGames.SaveGamePro.SaveGame.Load<T>(key);
    }

    public bool HasData(string key)
    {
        return reader.Exists(key);
        // return BayatGames.SaveGamePro.SaveGame.Exists(key);
    }


    [Button]
    public void LoadAllData()
    {
        foreach (GameData data in gameDatasList)
        {
            data.LoadData();
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
            data.NewData();
        }
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
