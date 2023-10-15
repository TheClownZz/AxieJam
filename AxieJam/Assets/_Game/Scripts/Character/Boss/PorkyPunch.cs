using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;

public class PorkyPuch : BossAttack
{
    const string attackEvent = "eAttack";
    [SerializeField] Transform hand;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float attackRange = 1.2f;

    public override void OnInits(Character control)
    {
        base.OnInits(control);
        eControl.GetCom<BossMove>().SetAllowMove(true);
    }
    protected override void OnAttack()
    {
        base.OnAttack();
        eControl.GetCom<BossMove>().SetAllowMove(false);
        anim.state.SetAnimation(0, animName, false);
    }

    public void OnDamePlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(hand.position, attackRange, playerLayer);
        if (hit)
        {
            Player _target = hit.GetComponent<Player>();
            if (_target)
            {
                float _damage = damage;
                bool isCrit = Random.value <= control.stat.critRate;
                if (isCrit)
                    _damage += _damage * control.stat.critDamage;
                _target.GetPCom<PlayerHp>().TakeDamage(_damage, isCrit);
            }
        }
    }

    protected override void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        base.HandleEvent(trackEntry, e);
        if (e.Data.Name == attackEvent)
        {
            OnDamePlayer();
        }
    }

    protected override void OnEndAnim(TrackEntry trackEntry)
    {
        base.OnEndAnim(trackEntry);
        if (trackEntry.Animation.Name == animName)
        {
            isAttack = false;
            eControl.GetCom<BossMove>().SetAllowMove(true);
            eControl.SetState(CharacterState.Run);
        }
    }

    protected override void OnCompleteAnim(TrackEntry trackEntry)
    {
        base.OnCompleteAnim(trackEntry);
        if (trackEntry.Animation.Name == animName)
        {
            isAttack = false;
            eControl.GetCom<BossMove>().SetAllowMove(true);
            eControl.SetState(CharacterState.Run);
        }
    }

    protected override bool CheckRange()
    {
        if (target.isDead)
            return false;
        return Vector2.Distance(hand.transform.position, target.transform.position) < range;
    }
}
