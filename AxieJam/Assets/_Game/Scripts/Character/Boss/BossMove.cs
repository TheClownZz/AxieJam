using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : EnemyMove
{
    bool isMove = true;
    public override void OnInits(Character e)
    {
        base.OnInits(e);
        isMove = true;
    }

    public override void OnUpdate(float dt)
    {
        if (isMove)
            base.OnUpdate(dt);
    }

    public void SetAllowMove(bool isMove)
    {
        this.isMove = isMove;
    }
}
