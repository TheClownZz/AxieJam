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
    const int delay = 3;


    public Player player;
    public GameState gameState;
    public GameConfig gameConfig;
    public Transform bulletSpawner;
    public LevelController levelController;

    [SerializeField] GameLevelAsset asset;

    public bool isCheat;

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

        int count = delay;
        ScreenGame screenInGame = UIManager.Instance.GetScreen<ScreenGame>();
        screenInGame.UpdateCountDown(count.ToString());
        SetGameState(GameState.Starting);

        DOVirtual.DelayedCall(1, () =>
        {
            count -= 1;
            if (count == 0)
            {
               // levelController.LoadLevel();
                SetGameState(GameState.Playing);
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

    public void OnLoss()
    {
        if (gameState != GameState.Playing)
            return;
        player.OnLose();
        levelController.OnLose();
        SetGameState(GameState.Ending);
        UIManager.Instance.ShowPopup<PopupGameOver>();
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
        player.isActive = gameState == GameState.Playing;
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

}
