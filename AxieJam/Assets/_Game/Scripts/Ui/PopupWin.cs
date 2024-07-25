using I2.TextAnimation;
using Spine.Unity;
using UnityEngine;

public class PopupWin : PopupBase
{
    [SerializeField] GameObject btnNext;
    [SerializeField] SkeletonGraphic anim;
    [SerializeField] string animName;
    [SerializeField] TextAnimation animText;
    public void SetAnim(SkeletonDataAsset asset)
    {
        anim.skeletonDataAsset = asset;
        anim.Initialize(true);
        anim.AnimationState.SetAnimation(0, animName, true);

    }

    public void SetNext(bool value)
    {
        //btnNext.SetActive(value);
    }
    public override void OnShow(float fadeTime = 0)
    {
        base.OnShow(fadeTime);
        animText.PlayAnim();
        AudioManager.Instance.PlayOnceShot(AudioType.WIN);
    }
    public void OnBtnBackClick()
    {
        OnHide();
        GameManager.Instance.ClearLevel();
        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
        UIManager.Instance.GetScreen<ScreenGame>().LoadMenu();
    }

    public void OnBtnNextClick()
    {
        OnHide();
        GameManager.Instance.ClearMap();
        GameManager.Instance.ResetAllPlayer();
        GameManager.Instance.StartLevel();

        AudioManager.Instance.PlayOnceShot(AudioType.CLICK);
    }

    
}

