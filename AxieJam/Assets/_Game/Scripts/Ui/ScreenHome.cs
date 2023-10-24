using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHome : ScreenBase
{
    const string firstPlay = "first";
    [SerializeField] TextMeshProUGUI tmpLoad;
    [SerializeField] GameObject panelContent;
    [SerializeField] GameObject panelLoad;
    [SerializeField] Image imgLoad;
    [SerializeField] List<ItemSelect> itemList;
    [HideInInspector] public List<ItemSelect> itemSelectedList = new List<ItemSelect>();
    [SerializeField] TeamAvtController teamAvtController;

    public override void OnShow()
    {
        base.OnShow();

        SetupSelectedList();
        foreach (var item in itemList)
        {
            item.UpdateUi();
            item.SetSelect(itemSelectedList.Contains(item));
        }
        imgLoad.fillAmount = 0;
        tmpLoad.SetText("");

        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        teamAvtController.UpdateTeam(team);
        panelContent.gameObject.SetActive(true);

        GameManager.Instance.ShowMap(false);
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

   
    public void OnBtnPlayClick()
    {
        tmpLoad.SetText("Loading...");
        panelLoad.SetActive(true);
        panelContent.SetActive(false);
        imgLoad.fillAmount = 0;
        int loadTime = 5;
        if (PlayerPrefs.GetInt(firstPlay, 0) == 0)
        {
            loadTime = 10;
            PlayerPrefs.SetInt(firstPlay, 1);
        }
        imgLoad.DOFillAmount(1, loadTime).OnComplete(() =>
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
        });

      
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);

    }

    public void OnBtnTeamClick()
    {
        OnHide();
        UIManager.Instance.ShowScreen<ScreenTeam>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
