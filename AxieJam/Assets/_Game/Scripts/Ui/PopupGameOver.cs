using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupGameOver : PopupBase
{
    [SerializeField] SkeletonGraphic anim;

    public void OnBtnBackClick()
    {
        OnHide();
        UIManager.Instance.HideScreen<ScreenGame>();
        UIManager.Instance.ShowScreen<ScreenHome>();
    }

    public override void OnHide(float fadeTime = 0)
    {
        base.OnHide(fadeTime);
      //  anim.AnimationState.SetAnimation(0, SpineKey.Idle, true);
    }
    public override void OnShow(float fadeTime = 0)
    {
        base.OnShow(fadeTime);
        AudioManager.Instance.PlayOnceShot(AudioType.LOSE);
      //  anim.AnimationState.SetAnimation(0, SpineKey.Die, false);
    }
}
