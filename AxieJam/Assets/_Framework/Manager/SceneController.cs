using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneController : MonoSingleton<SceneController>
{
    [SerializeField] string MenuScene = "MenuScene";
    [SerializeField] string GameScene = "GameScene";
    [SerializeField] List<AssetLoader> loaderList = new List<AssetLoader>();

    AssetLoader currentLoader;

    public void LoadMenu()
    {
        AssetLoader oldAsset = currentLoader;
        currentLoader = GetLoader(MenuScene);
        currentLoader.LoadAsset();
        if (oldAsset != null)
        {
            StartCoroutine(IUnLoad(oldAsset));
        }
        SceneManager.LoadScene(MenuScene);

    }
    public AsyncOperation LoadMenuAsync()
    {
        AssetLoader oldAsset = currentLoader;
        currentLoader = GetLoader(MenuScene);
        currentLoader.LoadAsset();
        if (oldAsset != null)
        {
            StartCoroutine(IUnLoad(oldAsset));
        }
        return SceneManager.LoadSceneAsync(MenuScene);

    }

    public AsyncOperation LoadGame()
    {
        AssetLoader oldAsset = currentLoader;
        currentLoader = GetLoader(GameScene);
        currentLoader.LoadAsset();
        if (oldAsset != null)
        {
            StartCoroutine(IUnLoad(oldAsset));
        }
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

    IEnumerator IUnLoad(AssetLoader loader)
    {
        yield return IsLoadAllRef();
        loader.UnLoadAsset(currentLoader);
    }
}
