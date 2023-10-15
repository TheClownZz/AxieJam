using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Gun
{
    float oneHitRate;

    public override void OnInits(Character characterControl)
    {
        base.OnInits(characterControl);
        oneHitRate = config.GetValue(WeaponSpiecialDataType.Rate, 0.01f);
    }
    protected override Bullet SpawnBullet()
    {
        OneHitBullet b = base.SpawnBullet().GetComponent<OneHitBullet>();
        b.SetOneHitRate(oneHitRate);
        return b;
    }
}
