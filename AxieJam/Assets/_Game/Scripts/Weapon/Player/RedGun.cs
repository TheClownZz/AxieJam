using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGun : PlayerGun
{
    float cachedCooldown;

    public override void ActiveSKill(SkillConfig config)
    {
        base.ActiveSKill(config);
        cachedCooldown = cooldown;
        cooldown /= config.GetSkillValue(SkillType.ActiveValue, 1.5f);
    }

    public override void DeAvtiveSkill()
    {
        base.DeAvtiveSkill();
        cooldown = cachedCooldown;
    }
}
