using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoSingleton<Master>
{
    #region CONST
    public const string AUDIO_SAVE = "AUDIO_SAVE";

    #endregion

    #region EDITOR PARAMS
    #endregion

    #region PARAMS
    public bool isMasterLoaded;
    public bool isMasterReady;
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    #endregion

    #region METHODS
    protected override void Initiate()
    {
        base.Initiate();

        DontDestroyOnLoad(gameObject);

        StartCoroutine(I_Initiate());
    }

    private IEnumerator I_Initiate()
    {
        isMasterLoaded = isMasterReady = false;

        yield return new WaitUntil(() => AudioManager.Instance != null);

        AudioManager.Instance.OnInit();


        yield return new WaitUntil(() => UIManager.Instance != null);

        UIManager.Instance.OnInit();

        yield return new WaitUntil(() => GameManager.Instance != null);

        GameManager.Instance.OnInit();


#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif
        isMasterReady = true;

    }

    #endregion

    #region DEBUG
    #endregion
}