using Spine.Unity;
using UnityEngine;

public class PopupGameOver : PopupBase
{
    [SerializeField] SkeletonGraphic anim;
    [SerializeField] string animName;
    public void OnBtnBackClick()
    {
        OnHide();
        GameManager.Instance.ClearLevel();
        UIManager.Instance.GetScreen<ScreenGame>().LoadMenu();

    }

    public void SetAnim(SkeletonDataAsset asset)
    {
        anim.skeletonDataAsset = asset;
        anim.Initialize(true);
        anim.AnimationState.SetAnimation(0, animName, true);

    }

    public override void OnShow(float fadeTime = 0)
    {
        base.OnShow(fadeTime);
        AudioManager.Instance.PlayOnceShot(AudioType.GAME_OVER);
    }
}
