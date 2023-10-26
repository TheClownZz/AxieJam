using DG.Tweening;
using I2.TextAnimation;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHome : ScreenBase
{
    const string firstPlay = "first";

    [SerializeField] TextAnimation textAnimation;
    [SerializeField] TextMeshProUGUI tmpLoad;
    [SerializeField] GameObject panelContent;
    [SerializeField] GameObject panelLoad;
    [SerializeField] Image imgLoad;

    [SerializeField] TeamAvtController teamAvtController;

    public override void OnShow()
    {
        base.OnShow();
        panelLoad.SetActive(false);
        panelContent.SetActive(true);
        imgLoad.fillAmount = 0;
        tmpLoad.SetText("");
        textAnimation.PlayAnim();
        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        teamAvtController.UpdateTeam(team);
        panelContent.gameObject.SetActive(true);

        GameManager.Instance.ShowMap(false);
    }


   
    public void OnBtnPlayClick()
    {
        tmpLoad.SetText("Loading...");
        panelLoad.SetActive(true);
        panelContent.SetActive(false);
        imgLoad.fillAmount = 0;
        int loadTime = 2;
        if (PlayerPrefs.GetInt(firstPlay, 0) == 0)
        {
            loadTime = 10;
            PlayerPrefs.SetInt(firstPlay, 1);
        }
        imgLoad.DOFillAmount(1, loadTime).OnComplete(() =>
        {
            OnHide();
            GameManager.Instance.ShowMap(true);
            GameManager.Instance.UpdatePlayerList();
            GameManager.Instance.StartLevel();
            UIManager.Instance.ShowScreen<ScreenGame>();
         
        });

      
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);

    }

    public void OnBtnTeamClick()
    {
        OnHide();
        UIManager.Instance.ShowScreen<ScreenTeam>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }

    public void OnBtnAxieClick()
    {
        OnHide();
        UIManager.Instance.ShowScreen<ScreenAxie>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
