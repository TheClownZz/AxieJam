using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneSwitcher : MonoSingleton<SceneSwitcher>
{
    [SerializeField] string MenuScene = "MenuScene";
    [SerializeField] string GameScene = "GameScene";
    [SerializeField] List<AssetLoader> loaderList = new List<AssetLoader>();

    AssetLoader currentLoader;

    public void LoadMenu()
    {
        currentLoader?.UnLoadAsset();
        currentLoader = GetLoader(MenuScene);
        currentLoader.LoadAsset(() =>
        {
            SceneManager.LoadScene(MenuScene);
        });
    }
    public AsyncOperation LoadMenuAsync()
    {
        currentLoader?.UnLoadAsset();
        currentLoader = GetLoader(MenuScene);
        currentLoader.LoadAsset();
        return SceneManager.LoadSceneAsync(MenuScene);

    }

    public AsyncOperation LoadGame()
    {
        currentLoader?.UnLoadAsset();
        currentLoader = GetLoader(GameScene);
        currentLoader.LoadAsset();
        return SceneManager.LoadSceneAsync(GameScene);
    }

    AssetLoader GetLoader(string sceneName)
    {
        return loaderList.Find(x => x.sceneName == sceneName);
    }

    public bool IsLoadAllRef()
    {
        return currentLoader.IsLoadAll();
    }
}
