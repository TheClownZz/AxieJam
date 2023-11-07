using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoSingleton<SceneController>
{
    const string MenuScene = "MenuScene";
    const string GameScene = "GameScene";

    public void LoadMenu()
    {
        SceneManager.LoadScene(MenuScene);
    }

    public AsyncOperation LoadGame()
    {
        return SceneManager.LoadSceneAsync(GameScene);
    }
}
