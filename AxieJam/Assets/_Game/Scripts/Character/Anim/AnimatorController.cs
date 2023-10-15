using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : BaseAnimController
{
    [SerializeField] Animator anim;
    CharacterState cachedState;
    string cachedKey;
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        anim = GetComponentInChildren<Animator>();
    }
#endif

    public override void SetAnim(CharacterState state)
    {
        if (cachedState == state)
            return;
        cachedState = state;
        base.SetAnim(state);
        switch (state)
        {
            case CharacterState.Alive:
            case CharacterState.Idle:
                anim.SetFloat(AnimKey.Speed, 0);
                anim.SetTrigger(AnimKey.Idle);
                cachedKey = AnimKey.Idle;
                break;
            case CharacterState.Die:
                anim.SetTrigger(AnimKey.Die);
                cachedKey = AnimKey.Die;
                break;
            case CharacterState.Run:
                anim.SetFloat(AnimKey.Speed, 1);
                cachedKey = AnimKey.Speed;
                break;
        }
    }

    public override void SetSpeed(float speed)
    {
        base.SetSpeed(speed);
        if (cachedKey == AnimKey.Speed)
            anim.SetFloat(cachedKey, speed);
    }
}
