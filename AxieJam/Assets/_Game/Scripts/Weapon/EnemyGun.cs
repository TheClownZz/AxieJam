using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Weapon
{
    public Vector3 targetPos;
    [SerializeField] protected Bullet bulletPf;
    [SerializeField] protected Transform shooter;
    [SerializeField] protected float butlletSpeed = 10f;
    [SerializeField] Sprite bulletSprite;

    public override void OnAttack()
    {
        base.OnAttack();
        SpawnBullet();
    }
    public Bullet SpawnBullet()
    {
        Vector3 dir = (targetPos - shooter.transform.position).normalized;
        Bullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<Bullet>();
        b.transform.SetParent(GameManager.Instance.bulletSpawner);
        b.transform.position = shooter.transform.position;
        b.transform.rotation = shooter.transform.rotation;
        b.OnInits(this, butlletSpeed, dir);
        b.SetSprite(bulletSprite);
        b.SetDamageRate(1);
        return b;
    }
}
