using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AnimationKey
{
    public static string IDLE = "Idle";
    public static string RUN = "Run";
    public static string JUMP = "Jump";
    public static string JUMP_DOWN = "JumpDown";
    public static string HIT_END = "HitEnd";
    public static string ATK_END = "AtkEnd";
    public static string ATK_1 = "Atk_1";
    public static string ATK_2 = "Atk_2";
    public static string ATK_3 = "Atk_3";
    public static string INTRO = "Intro";
    public static string DEAD = "Dead";
    public static string WALK = "Walk";
    public static string ROLL_BACK = "RollBack";
    public static string ROLL_LEFT = "RollLeft";
    public static string ROLL_RIGHT = "RollRight";
    public static string POWER = "Power";
    public static string POWER_ATK = "PowerAtk";
    public static string HIT = "Hit";

}

public class CharacterMecanim : MonoBehaviour
{
    #region CONST
    #endregion

    #region EDITOR PARAMS
    [SerializeField] private Animator cachedAnimator;

    #endregion

    #region PARAMS    
    [SerializeField] private string curAnimation;
    #endregion

    #region PROPERTIES
    #endregion

    #region EVENTS
    public System.Action<string, string> OnAnimationEvent = null;
    public System.Action<string> OnAnimationComplete = null;

    public Animator CachedAnimator { get => cachedAnimator; set => cachedAnimator = value; }
    #endregion

    #region METHODS
    public void PlayAnimation(string animName, float speed = 1.0f, bool isForce = false)
    {
        curAnimation = animName;
        CachedAnimator?.SetTrigger(animName);
        CachedAnimator?.SetFloat("Speed", speed);
    }

    [Sirenix.OdinInspector.MinMaxSlider(1, 10, true)]
    public void SetSpeed(float speed)
    {
        CachedAnimator?.SetFloat("Speed", speed);
    }

    public void SetFloat(string name, float value)
    {
        CachedAnimator?.SetFloat(name, value);
    }

    public void SetBool(string name, bool value)
    {
        CachedAnimator?.SetBool(name, value);
    }

    protected void AnimationEvent(string eventName)
    {
        OnAnimationEvent?.Invoke(curAnimation, eventName);
    }

    protected void AnimationComplete(string animationName)
    {
        OnAnimationComplete?.Invoke(animationName);
    }


#if UNITY_EDITOR
    protected void OnValidate()
    {
        if (CachedAnimator == null)
            CachedAnimator = gameObject.GetComponentInChildren<Animator>();
    }
#endif

    public void Reset()
    {
        curAnimation = string.Empty;
    }
    #endregion

    #region DEBUG
    #endregion
}
