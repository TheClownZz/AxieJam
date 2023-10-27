using DG.Tweening;
using Spine;
using UnityEngine;

public class EnemySpineController : SpineController
{
    [SerializeField] string Hit = "die";
    [SerializeField] string Attack = "die";
    [SerializeField] float attackTime = 1f;
    Tween attackTween;
    string attackAnim;
    public override void OnInits(Character control)
    {
        base.OnInits(control);
        anim.AnimationState.Complete += delegate (TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == Hit)
            {
                control.OnHitDone();
            }
            else if (trackEntry.Animation.Name == attackAnim)
            {
                control.GetCom<EnemyAttack>().OnAttackDone();
            }
        };

        anim.AnimationState.Start += delegate (TrackEntry trackEntry)
        {
            if (trackEntry.Animation.Name == attackAnim)
            {
                attackTween = GameManager.Instance.DelayedCall(attackTime, () =>
                {
                    control.GetCom<EnemyAttack>().Attacktarget();
                });
            }

        };

    }

    public override void OnDead()
    {
        base.OnDead();
        if (attackTween != null)
        {
            attackTween.Kill();
            attackTween = null;
        }
    }


    public override void SetAnim(CharacterState state)
    {
        switch (state)
        {
            case CharacterState.Hit:
                anim.state.SetAnimation(0, Hit, false);
                SetTimeScale(gameConfig.normalAnimSacle);
                break;
            case CharacterState.Attack:
                attackAnim = Attack;
                anim.state.SetAnimation(0, Attack, false);
                SetTimeScale(1f);
                break;
            default:
                break;
        }
        base.SetAnim(state);
    }

    public void SetAttack(string anim, float attackTime)
    {
        Attack = anim;
        this.attackTime = attackTime;
    }

}
