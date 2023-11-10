using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Collections;

public class ScreenGame : ScreenBase
{
    [SerializeField] ItemAvt mainItem;
    [SerializeField] Text tmpBackHome;
    [SerializeField] TextMeshProUGUI tmpMap;
    [SerializeField] TextMeshProUGUI tmpWave;
    [SerializeField] List<ItemAvt> itemAvtList;

    public override void OnShow()
    {
        base.OnShow();
        tmpBackHome.text = string.Empty;
    }
    public void UpdateWave(int current, int max)
    {
        tmpWave.SetText("{0}/{1}", current, max);
    }
    public void SetMap(int lv)
    {
        tmpMap.SetText("lv. {0}", lv);
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
        GameManager.Instance.ClearLevel();
        GameManager.Instance.SetGameState(GameState.Ready);
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
        UIManager.Instance.GetScreen<ScreenGame>().LoadMenu();
    }

    public void OnBtnGuideClick()
    {
        UIManager.Instance.ShowPopup<PopupGuide>();
    }

    public void LoadMenu()
    {
        tmpBackHome.text = "Back Home";
        AsyncOperation async = SceneSwitcher.Instance.LoadMenuAsync();
        async.allowSceneActivation = false;
        StartCoroutine(ICompleteLoad(async));
    }

    IEnumerator ICompleteLoad(AsyncOperation async)
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => async.progress == 0.9f && SceneSwitcher.Instance.IsLoadAllRef());
        async.allowSceneActivation = true;
    }
}
