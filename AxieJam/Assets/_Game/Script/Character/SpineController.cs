using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public static class SpineKey
{
    public static string Run = "run";
    public static string Idle = "idle";
    public static string Die = "die";
}
public class SpineController : MonoBehaviour
{
    [SerializeField] float speedFactor = 1;
    [SerializeField] SkeletonAnimation anim;

    string cachedKey;
    float cachedSpeed = 1;
    CharacterState cachedState;
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        anim = GetComponentInChildren<SkeletonAnimation>();
    }
#endif

    public  void OnInits()
    {
        anim.timeScale = cachedSpeed;
    }

    public void SetSpeed(float speed)
    {
        if (cachedState == CharacterState.Die || cachedKey != AnimKey.Speed)
            return;
        anim.timeScale = speed * speedFactor;
    }



    public void SetAnim(CharacterState state)
    {
        cachedState = state;
        switch (state)
        {
            case CharacterState.Alive:
            case CharacterState.Idle:
                anim.state.SetAnimation(0, SpineKey.Idle, true);
                cachedKey = SpineKey.Idle;
                anim.timeScale = 1;
                break;
            case CharacterState.Die:
                anim.state.SetAnimation(0, SpineKey.Die, false);
                cachedKey = SpineKey.Die;
                anim.timeScale = 1;

                break;
            case CharacterState.Run:
                anim.state.SetAnimation(0, SpineKey.Run, true);
                cachedKey = SpineKey.Run;
                break;
            default:
                cachedKey = string.Empty;
                break;
        }
    }

    public void FlipX(float flip)
    {
        anim.skeleton.ScaleX = flip;
    }

    public void Pause()
    {
        cachedSpeed = anim.timeScale;
        anim.timeScale = 0;
    }
    public void Resume()
    {
        anim.timeScale = cachedSpeed;
    }
}
