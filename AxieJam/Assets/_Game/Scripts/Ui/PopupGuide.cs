
using UnityEngine;
using UnityEngine.Events;

public class PopupGuide : PopupBase
{
    public UnityEvent hideCallBack;
    public override void OnShow(float fadeTime = 0)
    {
        base.OnShow(fadeTime);
        Time.timeScale = 0;
    }

    public override void OnHide(float fadeTime = 0)
    {
        base.OnHide(fadeTime);
        Time.timeScale = 1;
        hideCallBack.Invoke();
        hideCallBack.RemoveAllListeners();
    }
    public void OnBtnOkClick()
    {
        OnHide();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
