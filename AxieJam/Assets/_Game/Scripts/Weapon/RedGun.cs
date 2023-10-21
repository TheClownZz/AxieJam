using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGun : PlayerGun
{
    float cachedCooldown;
    float cachedDamageRate;

    public override void ActiveSKill(PlayerSkillConfig config)
    {
        base.ActiveSKill(config);
        cachedCooldown = cooldown;
        cachedDamageRate = damageRate;
        cooldown /= config.GetSkillValue(SkillType.ActiveValue, 1.5f);
        damageRate *= config.GetSkillValue(SkillType.Damage, 0.5f);
    }

    public override void DeAvtiveSkill()
    {
        base.DeAvtiveSkill();
        cooldown = cachedCooldown;
        damageRate = cachedDamageRate;
    }
}
