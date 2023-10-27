using I2.TextAnimation;
using UnityEngine;

public class PopupContinue : PopupBase
{
    [SerializeField] TextAnimation textAnimation;
    public override void OnShow(float fadeTime = 0)
    {
        base.OnShow(fadeTime);
        textAnimation.PlayAnim();
    }
    public void OnBtnCloseClick()
    {
        OnHide();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }
}
