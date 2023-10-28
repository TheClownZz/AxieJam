using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
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
    public GameObject objMap;

    [SerializeField] List<LevelAsset> assetList;

    protected Player currentPlayer;
    [HideInInspector] public List<Player> playerList;
    public Player GetCurrentPlayer() { return currentPlayer; }

    int mapIndex = 0;

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
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.P))
        {
            EditorApplication.isPaused = true;
        }
#endif
    }
    public void UpdatePlayerList()
    {
        List<PlayerType> team = DataManager.Instance.GetData<DataUser>().GetTeam();
        var assetList = DataManager.Instance.GetAsset<PlayerListAsset>();

        foreach (var playerType in team)
        {
            var asset = assetList.GetAsset(playerType);
            Player p = Instantiate(asset.prefab).GetComponent<Player>();
            p.transform.position = outPos;
            p.SetData(asset.data);
            p.OnInit();
            p.transform.SetParent(GetMapTf());
            playerList.Add(p);
        }

        currentPlayer = playerList[0];
        currentPlayer.transform.position = Vector3.zero;
    }


    public void SetPlayer(int index)
    {
        Vector3 pos = currentPlayer.transform.position;
        currentPlayer.transform.position = outPos;
        currentPlayer.OnUnSelect();
        currentPlayer = playerList[index];
        currentPlayer.transform.position = pos;
        currentPlayer.OnSelect();

    }

    public void ResetAllPlayer()
    {
        foreach(var p in playerList)
        {
            p.GetCom<PlayerHp>().RegenPercen(1);
        }
    }
    public void StartLevel()
    {
        mapIndex = (DataManager.Instance.GetData<DataLevel>().CurrentLevelId - 1);
        UIManager.Instance.GetScreen<ScreenGame>().SetMap(mapIndex + 1);
        currentPlayer.OnSelect();
        SetGameState(GameState.Playing);
        levelController.SetAsset(assetList[mapIndex]);
        levelController.LoadLevel();
    }



    public void OnWinMap()
    {
        SetGameState(GameState.Ready);
        DataManager.Instance.GetData<DataLevel>().SetNextLevel();
        UIManager.Instance.ShowPopup<PopupWin>().SetAnim(currentPlayer.spineController.GetAsset());
        UIManager.Instance.ShowPopup<PopupWin>().SetNext(!CheckMaxLevel());
    }
    public void ClearLevel()
    {
        foreach (var p in playerList)
        {
            Destroy(p.gameObject);
        }
        playerList.Clear();
        ClearMap();
    }
    public void ClearMap()
    {
        levelController.ClearCurrentLevel();
        ClearBullet();

        int index = 0;
        for (int i = objMap.transform.GetChild(index).childCount - 1; i >= 0; i--)
        {
            Destroy(objMap.transform.GetChild(index).GetChild(i).gameObject);
        }

        index = 1;
        for (int i = objMap.transform.GetChild(index).childCount - 1; i >= 0; i--)
        {
            PoolManager.Instance.DespawnObject(objMap.transform.GetChild(index).GetChild(i).transform);
        }
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
        AudioManager.Instance.PlayOnceShot(AudioType.GAME_OVER);
        UIManager.Instance.ShowPopup<PopupGameOver>().SetAnim(currentPlayer.spineController.GetAsset());
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        AudioManager.Instance.SetMusic(gameState == GameState.Ready);

    }

    public void ShowMap(bool isShow)
    {
        objMap.SetActive(isShow);
    }

    public Transform GetMapTf()
    {
        return objMap.transform;
    }


    public Tween DelayedCall(float time, TweenCallback callback)
    {

        return DOVirtual.DelayedCall(time, () =>
        {
            if (gameState == GameState.Playing)
                callback();
        });
    }

    public bool CheckMaxLevel()
    {
        return DataManager.Instance.GetData<DataLevel>().CurrentLevelId > assetList.Count;
    }
}
