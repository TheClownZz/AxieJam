using System.Collections.Generic;
using UnityEngine;

public enum CurrencyType
{
    Gold,
}

public class DataCurrency : GameData
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    [SerializeField]
    private CurrencySave saveData;
    #endregion

    #region PARAMS    
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    /*
     * Callback
     * <CurrencyType>   currency type
     * <int>            new value (had updated)
     * <bool>           has effect
     */
    public System.Action<CurrencyType, int, bool> OnValueChange = null;

    public CurrencySave DataSave { get => saveData; set => saveData = value; }
    #endregion

    #region METHODS

    public void Credit(CurrencyType type, int value, bool hasEffect = false, string source = "")
    {
        if (value == 0)
            return;
        byte index = (byte)type;
        if (index >= saveData.dataList.Count)
        {
            int temp = (index - saveData.dataList.Count) + 1;
            while (temp > 0)
            {
                saveData.dataList.Add(0);
                temp--;
            }
        }
        this.saveData.dataList[index] += value;
        this.OnValueChange?.Invoke(type, this.saveData.dataList[index], hasEffect);
    }

    public bool Debit(CurrencyType type, int value, bool hasEffect = false, string reason = "")
    {
        if (Enough(type, value))
        {
            byte index = (byte)type;
            int newValue = this.saveData.dataList[index];
            newValue -= value;
            this.saveData.dataList[index] = newValue;

            this.OnValueChange?.Invoke(type, newValue, hasEffect);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Enough(CurrencyType type, int value)
    {
        byte index = (byte)type;
        return this.saveData.dataList[index] >= value;
    }

    public bool Enough(byte typeId, int value)
    {
        return Enough((CurrencyType)typeId, value);
    }

    public int GetCurrency(CurrencyType type)
    {
        byte index = (byte)type;
        return this.saveData.dataList[index];
    }

    public override void SaveData()
    {
        DataManager.Instance.SaveData<CurrencySave>(GetType().FullName, saveData);
    }

    public override void LoadData()
    {
        saveData = DataManager.Instance.LoadData<CurrencySave>(GetType().FullName);
    }

    public override void NewData()
    {
        saveData = new CurrencySave();

        saveData.dataList = new List<int>();
        saveData.dataList.Add(0);
        SaveData();
    }

    public override bool HasData()
    {
        return !(saveData == null || saveData.dataList == null || saveData.dataList.Count == 0);
    }
    #endregion

    #region DEBUG
    #endregion
}