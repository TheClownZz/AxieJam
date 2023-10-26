using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelect : PopupBase
{
    [SerializeField] List<Button> buttonList;
    [SerializeField] TeamAvtController teamAvtController;
    public override void OnInit()
    {
        base.OnInit();
        for (int i = 0; i < buttonList.Count; i++)
        {
            int index = i;
            buttonList[i].onClick.AddListener(() =>
            {
                OnSelect(index);
                AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
            });
        }
    }

    public override void OnShow(float fadeTime = 0)
    {
        base.OnShow(fadeTime);
        UpdateUi();

        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        teamAvtController.UpdateTeam(team);
        Time.timeScale = 0;
    }

    public override void OnHide(float fadeTime = 0)
    {
        base.OnHide(fadeTime);
        Time.timeScale = 1;
    }
    private void OnSelect(int index)
    {
        GameManager.Instance.SetPlayer(index);
        UpdateUi();
        UIManager.Instance.GetScreen<ScreenGame>().UpdateAvt();
        OnHide(0);
    }

    private void UpdateUi()
    {
        var playerList = GameManager.Instance.playerList;
        var currentPlayer = GameManager.Instance.currentPlayer;

        for (int i = 0; i < playerList.Count; i++)
        {
            bool isSelected = playerList[i] == currentPlayer;

            //buttonList[i].interactable = !isSelected;
            buttonList[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = isSelected? "Selected": "Select";
        }
    }

   
}
