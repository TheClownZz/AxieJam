using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemykamikazeAttack : EnemyAttack
{
    public override void OnAttack()
    {
        control.SetState(CharacterState.Attack);
        control.DisableEnemy(true);
    }

    public override void Attacktarget()
    {
        base.Attacktarget();
        control.OnDead();
    }
}
