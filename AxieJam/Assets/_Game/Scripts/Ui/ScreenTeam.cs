using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTeam : ScreenBase
{
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] List<ItemSelect> itemList;
    [HideInInspector] public List<ItemSelect> itemSelectedList = new List<ItemSelect>();
    [SerializeField] TeamAvtController teamAvtController;
    List<PlayerType> currentTeam = new List<PlayerType>();
    public override void OnShow()
    {
        base.OnShow();
        SetupSelectedList();
        scrollRect.DOVerticalNormalizedPos(1, 1f).SetSpeedBased(true);
        foreach (var item in itemList)
        {
            item.UpdateUi();
            item.SetSelect(itemSelectedList.Contains(item));
        }
        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        currentTeam.AddRange(team);
        teamAvtController.UpdateTeam(team);
    }

    public void OnSelect(ItemSelect item)
    {
        itemSelectedList[itemSelectedList.Count - 1].SetSelect(false);
        itemSelectedList.RemoveAt(itemSelectedList.Count - 1);
        itemSelectedList.Insert(0, item);
        teamAvtController.UpdateTeam(itemSelectedList);
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
    public void OnBtnBackClick()
    {
        OnHide();
        UIManager.Instance.ShowScreen<ScreenHome>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }

    public void OnBtnConfirmClick()
    {
        OnHide();
        List<PlayerType> team = new List<PlayerType>();

        var itemSelectedList = UIManager.Instance.GetScreen<ScreenTeam>().itemSelectedList;
        foreach (var item in itemSelectedList)
        {
            team.Add(item.playerType);
        }
        DataManager.Instance.GetData<DataUser>().SetTeam(team);
        UIManager.Instance.ShowScreen<ScreenHome>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
