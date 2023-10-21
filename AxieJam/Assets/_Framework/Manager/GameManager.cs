using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public enum GameState
{
    Ready,
    Playing,
}
public class GameManager : MonoSingleton<GameManager>
{
    public GameState gameState;
    public GameConfig gameConfig;
    public Transform bulletSpawner;
    public LevelController levelController;

    public bool isCheat;
    public Player currentPlayer;
    public List<Player> playerList;

    [SerializeField] GameLevelAsset asset;
    [SerializeField] GameObject objMap;

    Vector3 outPos = new Vector3(9999, 9999, 9999);
    public bool isPause { get { return Time.timeScale == 0; } }
    protected override void Initiate()
    {
        base.Initiate();
        StartCoroutine(I_Initiate());
    }

    private IEnumerator I_Initiate()
    {
        yield return new WaitUntil(() => Master.Instance.isMasterReady);
        OnInit();
    }
    void OnInit()
    {
        levelController.OnInits();
        levelController.SetAsset(asset);
        SetGameState(GameState.Ready);
        UIManager.Instance.ShowScreen<ScreenHome>();
    }

    private void Update()
    {
        if (gameState == GameState.Playing && !isPause)
        {
            currentPlayer.OnUpdate(Time.deltaTime);
            levelController.OnUpdate(Time.deltaTime);
        }
    }
    public void UpdatePlayerList(List<ItemSelect> itemSelectList)
    {
        foreach (var p in playerList)
        {
            Destroy(p.gameObject);
        }
        playerList.Clear();
        var assetList = DataManager.Instance.GetAsset<PlayerListAsset>();

        foreach (var item in itemSelectList)
        {
            var asset = assetList.GetAsset(item.playerType);
            Player p = Instantiate(asset.prefab).GetComponent<Player>();
            p.transform.position = outPos;
            p.SetData(asset.data);
            p.OnInit();
            p.transform.SetParent(objMap.transform);
            playerList.Add(p);
        }

        currentPlayer = playerList[0];
        currentPlayer.transform.position = Vector3.zero;
    }


    public void SetPlayer(int index)
    {
        Vector3 pos = currentPlayer.transform.position;
        currentPlayer.OnUnSelect();
        currentPlayer = playerList[index];
        currentPlayer.transform.position = pos;
        currentPlayer.OnSelect();

    }


    public void StartLevel()
    {
        currentPlayer.OnSelect();
        SetGameState(GameState.Playing);
        levelController.LoadLevel();
    }

    public void ClearLevel()
    {
        levelController.DestroyCurrentLevel();
        ClearBullet();
    }

    private void ClearBullet()
    {
        for (int i = bulletSpawner.childCount - 1; i >= 0; i--)
        {
            bulletSpawner.GetChild(i).GetComponent<Bullet>().Clear();
        }
    }

    public void OnLoss()
    {
        if (gameState != GameState.Playing)
            return;
        currentPlayer.OnLose();
        levelController.OnLose();
        SetGameState(GameState.Ready);
        UIManager.Instance.ShowPopup<PopupGameOver>().SetAnim(currentPlayer.spineController.GetAsset());
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }

    public void ShowMap(bool isShow)
    {
        objMap.SetActive(isShow);
    }


}
