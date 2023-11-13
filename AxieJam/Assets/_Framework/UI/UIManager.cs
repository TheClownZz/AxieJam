using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{


    #region Editor paramters
    [SerializeField]
    private ScreenBase[] arrScreens = new ScreenBase[0];
    [SerializeField]
    private Dictionary<string, ScreenBase> dicScreens = new Dictionary<string, ScreenBase>();
    [SerializeField]
    protected PopupBase[] arrPopups = new PopupBase[0];
    [SerializeField]
    protected Dictionary<string, PopupBase> dicPopups = new Dictionary<string, PopupBase>();

    [SerializeField] ScreenBase activeScene;
    #endregion

    [HideInInspector] public bool isInit = false;
    IEnumerator Start()
    {
        yield return new WaitUntil(() => SceneController.Instance.IsLoadAllRef());
        OnInit();
        GetComponent<Canvas>().enabled = true;
    }

    private void OnDestroy()
    {
        m_Instance = null;
    }
    public void OnInit()
    {

        for (int i = 0; i < arrScreens.Length; i++)
        {
            var screen = arrScreens[i];
            var screenName = screen.GetName();

            if (!dicScreens.ContainsKey(screenName))
            {
                dicScreens.Add(screenName, screen);
            }
            else
            {
                Debug.LogError("Invalid screen name " + screenName + " of gameObject " + screen.gameObject.name);
            }
        }

        for (int i = 0; i < arrScreens.Length; i++)
        {
            arrScreens[i].OnInit();
        }

        for (int i = 0; i < arrPopups.Length; i++)
        {
            var popup = arrPopups[i];
            var popupName = popup.GetName();
            if (!dicPopups.ContainsKey(popupName))
            {
                dicPopups.Add(popupName, popup);
            }
        }

        for (int i = 0; i < arrPopups.Length; i++)
        {
            arrPopups[i].OnInit();
        }

        activeScene?.OnShow();
        isInit = true;
    }


    public void OnRelease()
    {
        for (int i = 0; i < arrScreens.Length; i++)
        {
            arrScreens[i].OnRelease();
        }
    }

    public T ShowScreen<T>() where T : ScreenBase
    {
        var screenName = typeof(T).FullName;

        if (dicScreens.ContainsKey(screenName))
        {
            dicScreens[screenName].OnShow();
            return dicScreens[screenName] as T;
        }

        Debug.LogError("Invalid screen " + screenName);
        return null;
    }

    public T HideScreen<T>() where T : ScreenBase
    {
        var screenName = typeof(T).FullName;

        if (dicScreens.ContainsKey(screenName))
        {
            dicScreens[screenName].OnHide();
            return dicScreens[screenName] as T;
        }

        // Debug.LogError("Invalid screen " + screenName);
        return null;
    }

    public T GetScreen<T>() where T : ScreenBase
    {
        var screenName = typeof(T).FullName;

        if (dicScreens.ContainsKey(screenName))
        {
            return dicScreens[screenName] as T;
        }

        // Debug.LogError("Invalid screen " + screenName);
        return null;
    }

    public T ShowPopup<T>(float fadeTime = 0) where T : PopupBase
    {
        var popupName = typeof(T).FullName;

        if (dicPopups.ContainsKey(popupName))
        {
            dicPopups[popupName].OnShow(fadeTime);
            return dicPopups[popupName] as T;
        }

        return null;
    }

    public T HidePopup<T>(float fadeTime = 0) where T : PopupBase
    {
        var popupName = typeof(T).FullName;

        if (dicPopups.ContainsKey(popupName))
        {
            dicPopups[popupName].OnHide(fadeTime);
            return dicPopups[popupName] as T;
        }

        return null;
    }

    public T GetPopup<T>() where T : PopupBase
    {
        var popupName = typeof(T).FullName;

        if (dicPopups.ContainsKey(popupName))
        {
            return dicPopups[popupName] as T;
        }

        return null;
    }

#if UNITY_ANDROID
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = arrPopups.Length - 1; i >= 0; i--)
            {
                if (arrPopups[i].OnBack())
                    return;
            }

            for (int i = arrScreens.Length - 1; i >= 0; i--)
            {
                if (arrScreens[i].OnBack())
                    return;
            }
        }
    }
#endif

#if UNITY_EDITOR
    public void OnValidate()
    {
        arrScreens = gameObject.GetComponentsInChildren<ScreenBase>();
        arrPopups = gameObject.GetComponentsInChildren<PopupBase>();
    }
#endif

}
