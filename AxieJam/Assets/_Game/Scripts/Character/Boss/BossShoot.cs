using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossShoot : EnemyShoot
{
    [SerializeField] int skillIndex = 0;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        BossSkillConfig config = control.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        coolDown = config.GetSkillValue(SkillType.Cooldown, 1);
    }
}
