using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class SpineController : MonoBehaviour
{

    [SerializeField] MeshRenderer meshRender;
    [SerializeField] float runScale = 1.5f;
    [SerializeField] SkeletonAnimation anim;
    [SerializeField] string Run = "run";
    [SerializeField] string Idle = "idle";
    [SerializeField] string Die = "die";
    [SerializeField] string Hit = "die";

    Character control;
    float cachedScale;

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
        meshRender = anim.GetComponent<MeshRenderer>();
    }
#endif

    public void OnInits(Character control)
    {
        this.control = control;
        SetTimeScale(1);
        anim.AnimationState.Complete += delegate (TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == Hit)
            {
                control.OnHitDone();
            }
        };
    }


    public void SetAnim(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Alive:
            case CharacterState.Idle:
                anim.state.SetAnimation(0, Idle, true);
                SetTimeScale(1);
                break;
            case CharacterState.Die:
                anim.state.SetAnimation(0, Die, false);
                SetTimeScale(1);

                break;
            case CharacterState.Run:
                anim.state.SetAnimation(0, Run, true);
                SetTimeScale(runScale);
                break;
            case CharacterState.Hit:
                anim.state.SetAnimation(0, Hit, false);
                SetTimeScale(1);
                break;
            default:
                break;
        }
        cachedScale = anim.timeScale;
    }

    public void FlipX(float flip)
    {
        anim.skeleton.ScaleX = flip;
    }


    public void ResetScale()
    {
        SetTimeScale(cachedScale);
    }
    public void Pause()
    {
        SetTimeScale(0);
    }
    public void Resume()
    {
        SetTimeScale(cachedScale);
    }

    public void ShowRender(bool isShow)
    {
        meshRender.enabled = isShow;
    }
    private void SetTimeScale(float timeScale)
    {
        anim.timeScale = timeScale;
    }
}
