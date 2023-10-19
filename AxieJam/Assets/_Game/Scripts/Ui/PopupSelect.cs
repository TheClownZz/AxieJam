using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupSelect : PopupBase
{
    [SerializeField] List<Image> iconList;
    [SerializeField] List<Button> buttonList;
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
    }

    private void UpdateUi()
    {
        var playerList = GameManager.Instance.playerList;
        var currentPlayer = GameManager.Instance.currentPlayer;

        for (int i = 0; i < playerList.Count; i++)
        {
            iconList[i].sprite = DataManager.Instance.GetAsset<PlayerListAsset>().GetAsset(playerList[i].type).data.avatar;
            bool isSelected = playerList[i] == currentPlayer;

            buttonList[i].interactable = !isSelected;
            buttonList[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = isSelected? "Selected": "Select";


        }
    }
}
