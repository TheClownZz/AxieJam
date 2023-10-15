using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierceGun : Gun
{
    float pierceRate = 0.16f;

    public override void OnInits(Character characterControl)
    {
        base.OnInits(characterControl);
        pierceRate = config.GetValue(WeaponSpiecialDataType.Rate, 0.16f);
    }
    protected override Bullet SpawnBullet()
    {
        PierceBullet b = base.SpawnBullet().GetComponent<PierceBullet>();
        b.rate = pierceRate;
        return b;
    }
}
