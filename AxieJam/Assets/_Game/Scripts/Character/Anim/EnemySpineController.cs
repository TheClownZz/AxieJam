using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpineController : SpineController
{

    [SerializeField] string Hit = "die";
    [SerializeField] string Attack = "die";
    public override void OnInits(Character control)
    {
        base.OnInits(control);
        anim.AnimationState.Complete += delegate (TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == Hit)
            {
                control.OnHitDone();
            }
            else if (trackEntry.Animation.Name == Attack)
            {
                control.GetCom<EnemyAttack>().Attacktarget();
            }
        };

    }


    public override void SetAnim(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Hit:
                anim.state.SetAnimation(0, Hit, false);
                SetTimeScale(1);
                break;
            case CharacterState.Attack:
                anim.state.SetAnimation(0, Attack, false);
                break;
            default:
                break;
        }
        base.SetAnim(state);
    }


}
