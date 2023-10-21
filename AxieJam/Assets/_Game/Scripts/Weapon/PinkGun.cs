using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGun : PlayerGun
{
    float scale = 1f;
    public override void ActiveSKill(PlayerSkillConfig config)
    {
        base.ActiveSKill(config);
        scale *= config.GetSkillValue(SkillType.Size, 1.5f);
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
    }
}
