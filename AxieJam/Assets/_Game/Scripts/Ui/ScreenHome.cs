using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHome : ScreenBase
{
    [SerializeField] Button btnPlay;
    [SerializeField] List<ItemSelect> itemList;
    public List<ItemSelect> itemSelectedList = new List<ItemSelect>();
    public override void OnInit()
    {
        base.OnInit();
        btnPlay.onClick.AddListener(OnBtnPlayClick);
    }

    public override void OnShow()
    {
        base.OnShow();
        SetupSelectedList();
        foreach (var item in itemList)
        {
            item.UpdateUi();
            item.SetSelect(itemSelectedList.Contains(item));
        }
        GameManager.Instance.ShowMap(false);
    }
    public void OnBtnPlayClick()
    {
        OnHide();
        GameManager.Instance.ShowMap(true);
        GameManager.Instance.UpdatePlayerList(itemSelectedList);
        GameManager.Instance.StartLevel();
        UIManager.Instance.ShowScreen<ScreenGame>();
        List<PlayerType> team = new List<PlayerType>();
        foreach (var item in itemSelectedList)
        {
            team.Add(item.playerType);
        }
        DataManager.Instance.GetData<DataUser>().SetTeam(team);
    }

    private void SetupSelectedList()
    {
        itemSelectedList.Clear();
        var playerSelectedList = DataManager.Instance.GetData<DataUser>().GetTeam();
        foreach (var p in playerSelectedList)
        {
            foreach (var item in itemList)
            {
                if (item.playerType == p)
                    itemSelectedList.Add(item);
            }
        }
    }
    public void OnSelect(ItemSelect item)
    {
        itemSelectedList[itemSelectedList.Count - 1].SetSelect(false);
        itemSelectedList.RemoveAt(itemSelectedList.Count - 1);
        itemSelectedList.Insert(0, item);
    }

}
