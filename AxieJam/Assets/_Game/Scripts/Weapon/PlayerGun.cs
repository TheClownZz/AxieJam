using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Weapon
{
    [SerializeField] protected Bullet bulletPf;
    [SerializeField] protected Transform shooter;
    [SerializeField] protected float butlletSpeed = 10f;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] AudioClip hitClip;
    [SerializeField] float force = 50;
    public override void OnAttack()
    {
        base.OnAttack();
        SpawnBullet();
        characterControl.GetCom<PlayerMove>().ForceBack(force * transform.right);
    }

    protected virtual Bullet SpawnBullet()
    {
        Bullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<Bullet>();
        b.transform.SetParent(GameManager.Instance.bulletSpawner);
        b.transform.position = shooter.transform.position;
        b.transform.rotation = shooter.transform.rotation;
        b.OnInits(this, butlletSpeed, -transform.right);
        b.SetSprite(bulletSprite);
        b.SetHitClip(hitClip);
        b.SetDamageRate(1);
        return b;
    }
}
