using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public override float TakeDamage(float damage, bool isCrit)
    {
        return GetECom<EnemyHp>().TakeDamage(damage, isCrit);
    }

    public override void SetStat()
    {
        var data = GetComponent<SetupBossData>().asset.data;
        stat.SetHp(data.hp)
            .Setarmor(data.armor)
            .SetDamage(data.damage)
            .SetAttackSpeed(data.attackSpeed)
            .SetMoveSpeed(data.moveSpeed);
    }
}
