
using UnityEngine;

public class PopupGuide : PopupBase
{
    public override void OnShow(float fadeTime = 0)
    {
        base.OnShow(fadeTime);
        Time.timeScale = 0;
    }

    public override void OnHide(float fadeTime = 0)
    {
        base.OnHide(fadeTime);
        Time.timeScale = 1;
    }
    public void OnBtnOkClick()
    {
        OnHide();
        if(UIManager.Instance.GetScreen<ScreenHome>().isShowing)
        {
            PlayerPrefs.SetInt(GameConfig.showGuide, 1);
            UIManager.Instance.GetScreen<ScreenHome>().StartLoading();
        }
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);

    }
}
