using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGun : PlayerGun
{
    float scale = 1f;
    float cachedDamageRate;
    public override void ActiveSKill(PlayerSkillConfig config)
    {
        base.ActiveSKill(config);
        scale *= config.GetSkillValue(SkillType.Size, 1.5f);
        cachedDamageRate = damageRate;
        damageRate *= config.GetSkillValue(SkillType.Damage, 0.5f);
    }

    protected override Bullet SpawnBullet()
    {
        Bullet bullet = base.SpawnBullet();
        bullet.transform.localScale = Vector3.one * scale;
        return bullet;
    }

    public override void DeAvtiveSkill()
    {
        base.DeAvtiveSkill();
        scale = 1;
        damageRate = cachedDamageRate;
    }
}
