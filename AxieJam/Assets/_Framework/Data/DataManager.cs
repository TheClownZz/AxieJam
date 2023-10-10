using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


public class DataManager : MonoSingleton<DataManager>
{

    public List<GameAsset> assetsList = new List<GameAsset>();
    public List<GameData> gameDatasList = new List<GameData>();

    protected override void Initiate()
    {
        base.Initiate();

        for (int i = 0; i < gameDatasList.Count; i++)
        {
            var data = gameDatasList[i];
            data.LoadData();
            if (!data.HasData())
            {
                data.NewData();
            }
            else if (data.HasUpdateData())
            {
                data.UpdateData();
                Debug.LogErrorFormat("{0} has Update!!", data.GetName());
            }
            data.Initiate();
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
            return gameDatasList.Find(x => x.GetType().FullName == typeof(T).FullName) as T;
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("Missing GameData: {0}", typeof(T).FullName);
            return null;
        }
    }

    public void SaveData<T>(string key, T userSaveData)
    {
        BayatGames.SaveGamePro.SaveGame.Save<T>(key, userSaveData);
    }

    public T LoadData<T>(string key)
    {
        return BayatGames.SaveGamePro.SaveGame.Load<T>(key);
    }

    public bool HasData(string key)
    {
        return BayatGames.SaveGamePro.SaveGame.Exists(key);
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
