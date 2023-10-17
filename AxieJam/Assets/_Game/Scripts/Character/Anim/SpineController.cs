using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;


public class SpineController : MonoBehaviour
{
    [SerializeField] float runScale = 1.5f;
    [SerializeField] SkeletonAnimation anim;
    [SerializeField] string Run = "run";
    [SerializeField] string Idle = "idle";
    [SerializeField] string Die = "die";

    float cachedScale;
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
    }
#endif

    public void OnInits()
    {
        anim.timeScale = 1;
    }


    public void SetAnim(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Alive:
            case CharacterState.Idle:
                anim.state.SetAnimation(0, Idle, true);
                anim.timeScale = 1;
                break;
            case CharacterState.Die:
                anim.state.SetAnimation(0, Die, false);
                anim.timeScale = 1;

                break;
            case CharacterState.Run:
                anim.state.SetAnimation(0, Run, true);
                anim.timeScale = runScale;
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

    public void SetScale(float value )
    {
        anim.timeScale = value;
    }

    public void ResetScale()
    {
        anim.timeScale = cachedScale;
    }
    public void Pause()
    {
        anim.timeScale = 0;
    }
    public void Resume()
    {
        anim.timeScale = cachedScale;
    }
}
