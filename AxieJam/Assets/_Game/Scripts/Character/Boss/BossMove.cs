using DG.Tweening;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BossMove : EnemyShootMove
{
    [SerializeField] protected SkeletonAnimation anim;
    [SerializeField] string runAnim;
    [SerializeField] string moveEvent;
    [SerializeField] float jumpTime = 1f;
    [SerializeField] bool isJump = false;

    public override void OnInits(Character e)
    {
        base.OnInits(e);
        anim.AnimationState.Event += HandleEvent;

    }
    void HandleEvent(TrackEntry trackEntry, Spine.Event e)
    {
        // Play some sound if the event named "footstep" fired.
        if (e.Data.Name == moveEvent)
        {
            isJump = true;
            DOVirtual.DelayedCall(jumpTime, () => { isJump = false; });
        }
    }
    protected override void UpdatePostion(float dt)
    {
        if (!isJump) return;
        base.UpdatePostion(dt);
    }

    protected override void Force(float dt)
    {
       
    }
}
