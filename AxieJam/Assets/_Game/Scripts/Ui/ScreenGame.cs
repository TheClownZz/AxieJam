using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScreenGame : ScreenBase
{
    [SerializeField] TextMeshProUGUI tmpWave;
    [SerializeField] ItemAvt mainItem;
    [SerializeField] List<ItemAvt> itemAvtList;

    public override void OnShow()
    {
        base.OnShow();
        UpdateAvt();
    }

    public void UpdateWave(int current, int max)
    {
        tmpWave.SetText("Wave:{0}/{1}", current, max);
    }


    public void UpdateAvt()
    {
        var playerList = GameManager.Instance.playerList;
        var currentPlayer = GameManager.Instance.currentPlayer;

        var assetList = DataManager.Instance.GetAsset<PlayerListAsset>();
        mainItem.SetAvt(assetList.GetAsset(currentPlayer.type).data.avatar);
        currentPlayer.SetItemAvt(mainItem);
        int index = 0;
        foreach(var p in playerList)
        {
            if (p == currentPlayer)
                continue;
            itemAvtList[index].SetAvt(assetList.GetAsset(p.type).data.avatar);
            p.SetItemAvt(itemAvtList[index]);
            index += 1;
        }
    }

    private void Update()
    {
        if (isShowing && Input.GetKeyDown(KeyCode.Space) && 
            !UIManager.Instance.GetPopup<PopupSelect>().isShowing
            && GameManager.Instance.gameState == GameState.Playing)
        {
            UIManager.Instance.ShowPopup<PopupSelect>();
        }
    }

}
