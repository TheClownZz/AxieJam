using DG.Tweening;
using I2.TextAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenHome : ScreenBase
{

    [SerializeField] TextAnimation textAnimation;
    [SerializeField] Text tmpLoad;
    [SerializeField] GameObject panelContent;
    [SerializeField] Image imgLoad;
    [SerializeField] GameObject objNoti;

    [SerializeField] TeamAvtController teamAvtController;
    public override void OnShow()
    {
        base.OnShow();
        tmpLoad.text = "";
        imgLoad.fillAmount = 0;
        textAnimation.PlayAnim();
        panelContent.SetActive(true);

        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        teamAvtController.UpdateTeam(team);
        objNoti.SetActive(CheckNoti());
    }

    public void OnBtnPlayClick()
    {
        if (PlayerPrefs.GetInt(GameConfig.showGuide, 0) == 0)
        {
            UIManager.Instance.ShowPopup<PopupGuide>().hideCallBack.AddListener(() =>
            {
                PlayerPrefs.SetInt(GameConfig.showGuide, 1);
                StartLoading();
            });
        }
        else if (DataManager.Instance.GetData<DataLevel>().CheckMaxLevel())
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
        tmpLoad.text = "Loading...";
        imgLoad.fillAmount = 0;
        var async = SceneSwitcher.Instance.LoadGame();
        async.allowSceneActivation = false;
        imgLoad.DOFillAmount(1, loadTime).OnComplete(() =>
        {
            StartCoroutine(ICompleteLoad(async));
        });
    }

    IEnumerator ICompleteLoad(AsyncOperation async)
    {
        yield return new WaitUntil(() => async.progress == 0.9f);
        async.allowSceneActivation = true;
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
