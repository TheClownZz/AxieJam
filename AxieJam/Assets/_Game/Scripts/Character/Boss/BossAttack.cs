using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public class BossAttack : EnemyComponent
{
    protected Player target;
    protected Enemy eControl;
    [SerializeField] protected BossAttackConfig config;
    protected float damage;
    protected float range;
    protected float coolDown;

    protected float attackTime;

    public bool isAttack;
    public SkeletonAnimation anim;

     [SerializeField] protected string animName;

    [SerializeField] List<BossAttack> attackList;
    protected void Awake()
    {
        anim.AnimationState.Event += HandleEvent;
        anim.AnimationState.End += OnEndAnim;
        anim.AnimationState.Complete += OnCompleteAnim;
    }

    public override void OnInits(Character control)
    {
        base.OnInits(control);
        eControl = (Enemy)control;
        target = GameManager.Instance.player;
        damage = config.GetValue(BossDataType.damage, 1);
        range = config.GetValue(BossDataType.range, 1);
        coolDown = config.GetValue(BossDataType.coolDown, 5);
        attackTime = -coolDown;
        isAttack = false;
    }

    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
        if (CheckAttack() && CheckCoolDown() && CheckRange())
        {
            attackTime = Time.time;
            OnAttack();
        }
    }

    protected virtual void OnAttack()
    {
        isAttack = true;
        eControl.SetState(CharacterState.Attack);
    }

    protected virtual void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {

    }

    protected virtual void OnEndAnim(TrackEntry trackEntry)
    {

    }

    protected virtual void OnCompleteAnim(TrackEntry trackEntry)
    {

    }

    private bool CheckAttack()
    {
        foreach (var atk in attackList)
        {
            if (atk.isAttack)
                return false;
        }

        return !isAttack;
    }
    private bool CheckCoolDown()
    {
        return Time.time - attackTime > coolDown;
    }
    protected virtual bool CheckRange()
    {
        if (target.isDead)
            return false;
        return Vector2.Distance(eControl.transform.position, target.transform.position) < range;
    }
}
