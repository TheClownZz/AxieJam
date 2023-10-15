using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazokaGun : Gun
{
    [SerializeField] float radius = 2;
    protected override Bullet SpawnBullet()
    {
        BazokaBullet b = base.SpawnBullet().GetComponent<BazokaBullet>();
        b.SetRadius(radius);
        b.SetTarget(attckController.GetTarget().GetComponent<Character>());
        return b;
    }

}
