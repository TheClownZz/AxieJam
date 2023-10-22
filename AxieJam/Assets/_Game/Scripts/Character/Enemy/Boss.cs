using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] List<string> atkAnimList;
    [SerializeField] List<float> atkTimeList;

    int skillIndex = 0;
    public override void OnInit()
    {
        base.OnInit();
        ((EnemySpineController)spineController).SetAttack(atkAnimList[skillIndex], atkTimeList[skillIndex]);
    }
    public override float TakeDamage(float damage, bool isCrit)
    {
        EnemyHp eHp = GetECom<EnemyHp>();
        float hpLost = eHp.TakeDamage(damage, isCrit);
        if (skillIndex < atkAnimList.Count - 1 && eHp.currentHp < 0.5f * stat.hp)
        {
            skillIndex += 1;
            ((EnemySpineController)spineController).SetAttack(atkAnimList[skillIndex], atkTimeList[skillIndex]);
        }
        return hpLost;
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
