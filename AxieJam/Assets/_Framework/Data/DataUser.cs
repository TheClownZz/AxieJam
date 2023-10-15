using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataWeaponSave
{
    public WeaponType weaponType;
    public int tier;

    public DataWeaponSave(WeaponType weaponType, int tier)
    {
        this.weaponType = weaponType;
        this.tier = tier;
    }

    public void SetTier(int value)
    {
        tier = value;
        CheckTier();
    }

    public void CheckTier()
    {
        tier = Math.Min(tier, GameConfig.maxTier);
    }
}

[System.Serializable]
public class DataSaveUser
{

    public bool isVibrationOn;
    public bool isMusicOn;
    public bool isSoundOn;

    public List<DataWeaponSave> currentWeaponList;
    public List<bool> listSlot;
    public DataSaveUser(int maxSlot, int slotUnLock)
    {
        isVibrationOn = isMusicOn = isSoundOn = true;
        currentWeaponList = new List<DataWeaponSave>();
        currentWeaponList.Add(new DataWeaponSave(WeaponType.Pisol, 1));
        for (int i = 0; i < maxSlot - 1; i++)
        {
            currentWeaponList.Add(new DataWeaponSave(WeaponType.None, 0));
        }
        listSlot = new List<bool>();
        for (int i = 0; i < maxSlot; i++)
        {
            listSlot.Add(i <= slotUnLock - 1);

        }
    }

    public DataWeaponSave GetWeapon(WeaponType type)
    {
        return currentWeaponList.Find(x => x.weaponType == type);
    }

    public DataWeaponSave GetEmptySlot()
    {
        return currentWeaponList.Find(x => x.weaponType == WeaponType.None);
    }

    public void RemoveWeapon(WeaponType type)
    {
        int index = currentWeaponList.FindIndex(x => x.weaponType == type);
        if (index != -1)
        {
            for (int i = index; i < currentWeaponList.Count -1; i++)
            {
                currentWeaponList[i].tier = currentWeaponList[i+1].tier;
                currentWeaponList[i].weaponType = currentWeaponList[i+1].weaponType;
            }

            var data = currentWeaponList[currentWeaponList.Count - 1];
            data.weaponType = WeaponType.None;
            data.SetTier(0);
        }

    }

    public void ClearAllWeapon()
    {
        foreach (var weapon in currentWeaponList)
        {
            weapon.weaponType = WeaponType.None;
            weapon.SetTier(0);
        }
    }

    public void CheckTier()
    {
        foreach(var weapon in currentWeaponList)
        {
            weapon.CheckTier();
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
        if(dataSave != null)
        {
            dataSave.CheckTier();
        }
    }

    public override void NewData()
    {
        dataSave = new DataSaveUser(GameConfig.maxSlot, GameConfig.slotUnLock);
    }

    #region Setting
    public bool HasMusic()
    {
        return dataSave.isMusicOn;
    }

    public void SetMusic(bool isOn)
    {
        dataSave.isMusicOn = isOn;
        SaveData();
    }

    public bool HasSound()
    {
        return dataSave.isSoundOn;
    }


    public void SetSound(bool isOn)
    {
        dataSave.isSoundOn = isOn;
        SaveData();
    }

    public bool HasVibration()
    {
        return dataSave.isVibrationOn;
    }

    public void SetVibration(bool isOn)
    {
        dataSave.isVibrationOn = isOn;
        SaveData();
    }
    #endregion

    [Button]
    public void AddWeapon(WeaponType weaponType)
    {
        var wpSave = dataSave.GetWeapon(weaponType);
        if (wpSave != null)
        {
            wpSave.SetTier(wpSave.tier + 1);
        }
        else
        {
            var wpEmpty = dataSave.GetEmptySlot();
            wpEmpty.weaponType = weaponType;
            wpEmpty.SetTier(1);
        }
        SaveData();
    }

    public List<DataWeaponSave> GetCurrentWpList()
    {
        return dataSave.currentWeaponList;
    }
    public void RemoveWeapon(WeaponType weaponType)
    {
        dataSave.RemoveWeapon(weaponType);
        SaveData();
    }

    public bool IsUnLockSlot(int slotIndex)
    {
        return dataSave.listSlot[slotIndex];
    }

    public void UnlockSlot(int slotIndex)
    {
        dataSave.listSlot[slotIndex] = true;
        SaveData();
    }

    public int GetNumberWeapon()
    {
        int count = 0;
        foreach (var data in dataSave.currentWeaponList)
        {
            if (data.weaponType != WeaponType.None)
                count += 1;
        }

        return count;
    }

    public bool IsFullSlot()
    {
        int totalSlot = dataSave.listSlot.FindIndex(x => x == false);
        if (totalSlot == -1)
        {
            totalSlot = GameConfig.maxSlot;
        }

        return GetNumberWeapon() >= totalSlot;
    }

    public int GetTier(WeaponType type)
    {
        var weaponSave = dataSave.GetWeapon(type);
        if (weaponSave != null)
            return weaponSave.tier;
        else
            return 0;
    }

    public void ClearAllWeapon()
    {
        dataSave.ClearAllWeapon();
    }
}
