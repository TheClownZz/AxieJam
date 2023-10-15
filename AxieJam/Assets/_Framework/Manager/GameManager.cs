using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public enum GameState
{
    Starting,
    Playing,
    Ending,
}
public class GameManager : MonoSingleton<GameManager>
{
    public const int playerIndex = 0;
    const int delay = 3;


    public Player player;
    public GameState gameState;
    public GameConfig gameConfig;
    public Transform bulletSpawner;
    public LevelController levelController;

    public GameObject map;
    DataLevel dataLevel;
    [SerializeField] GameLevelAsset asset;

    public bool isCheat;

    protected override void Initiate()
    {
        base.Initiate();
        StartCoroutine(I_Initiate());
    }

    private IEnumerator I_Initiate()
    {
        yield return new WaitUntil(() => Master.Instance.isMasterLoaded);
        OnInit();
    }
    void OnInit()
    {
        dataLevel = DataManager.Instance.GetData<DataLevel>();
        asset = DataManager.Instance.GetAsset<GameLevelAsset>();

        player.OnInit();
        levelController.OnInits();
        levelController.SetAsset(asset);
        UIManager.Instance.ShowScreen<ScreenGame>();
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            player.OnUpdate(Time.deltaTime);
            levelController.OnUpdate(Time.deltaTime);
        }
    }


    public void StartLevel()
    {

        ClearLevel();

        player.StartLevel();
        Camera.main.GetComponent<CameraFollow>().ForceUpdate();
        levelController.OnLevelLoad(dataLevel.CurrentDay);

        int count = delay;
        ScreenGame screenInGame = UIManager.Instance.GetScreen<ScreenGame>();
        screenInGame.UpdateCountDown(count.ToString());
        screenInGame.SetInput(false);
        SetGameState(GameState.Starting);

        DOVirtual.DelayedCall(1, () =>
        {
            count -= 1;
            if (count == 0)
            {
                SetGameState(GameState.Playing);
                levelController.StartSpawn();
                screenInGame.SetInput(true);
                screenInGame.UpdateCountDown(string.Empty);
            }
            else
            {
                screenInGame.UpdateCountDown(count.ToString());

            }
        }, false).SetLoops(delay);

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
    public void OnWin()
    {
        if (gameState != GameState.Playing)
            return;
        PassDay();
        player.OnWin();
        levelController.OnWin();
        SetGameState(GameState.Ending);
        UIManager.Instance.ShowPopup<PopupWin>();

    }
    public void OnLoss()
    {
        if (gameState != GameState.Playing)
            return;
        ResetDay();
        player.OnLose();
        levelController.OnLose();
        SetGameState(GameState.Ending);
        UIManager.Instance.ShowPopup<PopupGameOver>();
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        player.isActive = gameState == GameState.Playing;
        UIManager.Instance.GetScreen<ScreenGame>().SetInput(gameState == GameState.Playing);
    }

    private void PassDay()
    {
        dataLevel.PassDay();
        if (dataLevel.CurrentDay > asset.dataList.Count)
        {
            dataLevel.PassLevel();
        }
    }


    public void OnReplay()
    {
        ResetPlayerHp();
        UIManager.Instance.HideScreen<ScreenGame>();
    }

    public void ResetPlayerHp()
    {
        player.GetCom<PlayerHp>().ResetHp();
    }

    private void ResetDay()
    {
        dataLevel.ResetDay();
        var dataUser = DataManager.Instance.GetData<DataUser>();
        dataUser.ClearAllWeapon();
        dataUser.AddWeapon(asset.weaponStart);
    }
}
