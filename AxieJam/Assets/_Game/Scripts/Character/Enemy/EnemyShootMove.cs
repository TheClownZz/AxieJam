using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootMove : EnemyMove
{
    const float stopMoveTime = 0.2f;
    bool allowMove;

    Tween stopMoveTween;
    public override void OnInits(Character e)
    {
        base.OnInits(e);
        allowMove = true;
        stopMoveTween = null;

    }
    protected override void Move(float dt)
    {
        Vector3 dir = Vector3.zero;
        Player attackTarget = control.GetCom<EnemyAttack>().target;
        dir = (GetTarget().transform.position - transform.position).normalized;
        if (!attackTarget)
        {
            allowMove = true;
        }
        else
        {
            if (allowMove)
            {
                if (stopMoveTween == null)
                {
                    stopMoveTween = DOVirtual.DelayedCall(stopMoveTime, () =>
                    {
                        allowMove = false;
                        stopMoveTween = null;
                    });
                }
            }

        }

        Facing(dir);

        direction = dir;
        if (allowMove)
        {
            UpdatePostion(dt);
        }
        UpdateState();

    }

}
