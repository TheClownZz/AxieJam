using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Sirenix.OdinInspector;

public class ScreenGame : ScreenBase
{
    [SerializeField] TextMeshProUGUI tmpWave;
    [SerializeField] ItemAvt mainItem;
    [SerializeField] List<ItemAvt> itemAvtList;
    [SerializeField] TextMeshProUGUI tmpMap;
    public override void OnShow()
    {
        base.OnShow();
        UpdateAvt();
    }

    public void UpdateWave(int current, int max)
    {
        tmpWave.SetText("{0}/{1}", current, max);
    }
    public void SetMap(int lv)
    {
        tmpMap.SetText("Map {0}", lv);
    }

    public void UpdateAvt()
    {
        var playerList = GameManager.Instance.playerList;
        var currentPlayer = GameManager.Instance.GetCurrentPlayer();

        var assetList = DataManager.Instance.GetAsset<PlayerListAsset>();
        mainItem.SetAvt(assetList.GetAsset(currentPlayer.type).avatar);
        currentPlayer.SetItemAvt(mainItem);
        int index = 0;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i] != currentPlayer)
            {
                itemAvtList[index].SetAvt(assetList.GetAsset(playerList[i].type).avatar);
                playerList[i].SetItemAvt(itemAvtList[index]);
                index += 1;
                if (index == playerList.Count)
                    break;
            }
        }

    }

    private void Update()
    {
        if (isShowing && Input.GetKeyDown(KeyCode.D) &&
            !UIManager.Instance.GetPopup<PopupSelect>().isShowing
            && GameManager.Instance.gameState == GameState.Playing)
        {
            UIManager.Instance.ShowPopup<PopupSelect>();
        }
    }

    public void OnBtnHomeClick()
    {
        OnHide();
        GameManager.Instance.ClearLevel();
        GameManager.Instance.SetGameState(GameState.Ready);
        UIManager.Instance.ShowScreen<ScreenHome>();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
