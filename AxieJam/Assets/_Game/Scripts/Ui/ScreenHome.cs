using DG.Tweening;
using I2.TextAnimation;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHome : ScreenBase
{
    const string showGuide = "showGuide";

    [SerializeField] TextAnimation textAnimation;
    [SerializeField] TextMeshProUGUI tmpLoad;
    [SerializeField] GameObject panelContent;
    [SerializeField] GameObject panelLoad;
    [SerializeField] Image imgLoad;

    [SerializeField] TeamAvtController teamAvtController;

    public override void OnShow()
    {
        base.OnShow();
        tmpLoad.SetText("");
        imgLoad.fillAmount = 0;
        textAnimation.PlayAnim();
        panelContent.SetActive(true);

        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        teamAvtController.UpdateTeam(team);
        panelContent.gameObject.SetActive(true);

        GameManager.Instance.ShowMap(false);
    }


    public void OnBtnOkClick()
    {
        StartLoading();
        PlayerPrefs.SetInt(showGuide, 1);
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }


    public void OnBtnPlayClick()
    {

        if (PlayerPrefs.GetInt(showGuide, 0) == 0)
        {
            panelLoad.SetActive(true);
        }
        else if(GameManager.Instance.CheckMaxLevel())
        {
            UIManager.Instance.ShowPopup<PopupContinue>();
        }else
        {
            StartLoading();

        }



        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);

    }

    private void StartLoading()
    {
        int loadTime = 3;

        panelLoad.SetActive(false);
        tmpLoad.SetText("Loading...");
        imgLoad.fillAmount = 0;
        imgLoad.DOFillAmount(1, loadTime).OnComplete(() =>
        {
            OnHide();
            GameManager.Instance.ShowMap(true);
            GameManager.Instance.UpdatePlayerList();
            GameManager.Instance.StartLevel();
            UIManager.Instance.ShowScreen<ScreenGame>();

        });
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
