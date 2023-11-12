using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneController : MonoSingleton<SceneController>
{
    [SerializeField] string MenuScene = "MenuScene";
    [SerializeField] string GameScene = "GameScene";
    [SerializeField] List<AssetLoader> loaderList = new List<AssetLoader>();

    AssetLoader currentLoader;

    private void Awake()
    {
        foreach(var loader in loaderList)
        {
            loader.Inits();
        }
    }
    public void LoadMenu()
    {
        currentLoader?.UnLoadAsset();
        currentLoader = GetLoader(MenuScene);
        currentLoader.LoadAsset();
        SceneManager.LoadScene(MenuScene);

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
        Debug.LogError("IsLoadAllRef");
        return currentLoader.IsLoadAll();
    }
}
