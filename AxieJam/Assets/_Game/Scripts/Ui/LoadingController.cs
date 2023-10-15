using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    [SerializeField] float loadingTime = 3;
    AsyncOperation async;

    private void Start()
    {
        StartCoroutine(ILoader());
    }
    IEnumerator ILoader()
    {
        async = SceneManager.LoadSceneAsync(ScecneConfig.MainScene);
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(loadingTime);

        yield return new WaitUntil(() => async.progress == 0.9f);

        async.allowSceneActivation = true;

    }
}
