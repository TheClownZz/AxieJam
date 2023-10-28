using DG.Tweening;
using I2.TextAnimation;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHome : ScreenBase
{

    [SerializeField] TextAnimation textAnimation;
    [SerializeField] TextMeshProUGUI tmpLoad;
    [SerializeField] GameObject panelContent;
    [SerializeField] Image imgLoad;
    [SerializeField] GameObject objNoti;

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

        GameManager.Instance.ShowMap(false);
        objNoti.SetActive(CheckNoti());
    }

    public void OnBtnPlayClick()
    {
        if (PlayerPrefs.GetInt(GameConfig.showGuide, 0) == 0)
        {
            UIManager.Instance.ShowPopup<PopupGuide>();
        }
        else if (GameManager.Instance.CheckMaxLevel())
        {
            UIManager.Instance.ShowPopup<PopupContinue>();
        }
        else
        {
            StartLoading();

        }



        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);

    }
    public void StartLoading()
    {
        int loadTime = 3;

        panelContent.SetActive(false);
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

    private bool CheckNoti()
    {
        bool isUpdate = false;
        for (int i = 0; i < (int)PlayerType.None; i++)
        {
            PlayerType playerType = (PlayerType)i;
            var data = DataManager.Instance.GetData<DataUser>().GetDataPlayer(playerType);
            var asset = DataManager.Instance.GetAsset<PlayerListAsset>().GetAsset(playerType);
            var levelConfig = asset.data.GetLevelConfig(data.level);
            var skillConfig = asset.data.GetSkillConfig(data.levelSkill);
            int footRequire = levelConfig.foodRequire;
            int potionRequire = skillConfig.defaultValue.potionRequire;

            isUpdate = data.potionCount >= potionRequire || data.foodCount >= footRequire;

            if (isUpdate)
                break;
        }

        return isUpdate;
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
