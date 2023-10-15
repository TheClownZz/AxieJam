using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] protected Bullet bulletPf;
    [SerializeField] protected Transform shooter;
    [SerializeField] protected float rotateSpeed = 1f;
    [SerializeField] protected float butlletSpeed = 10f;
    [SerializeField] protected int butlletMaxHit = 1;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] AudioClip hitClip;
    [SerializeField] PoolType hitType = PoolType.None;
    public override void OnClear()
    {
        base.OnClear();
    }

    public override void OnAttack()
    {
        base.OnAttack();
        SpawnBullet();
    }

    protected override void FaceToTarget(float dt)
    {
        base.FaceToTarget(dt);
        Vector2 dir = attckController.GetTarget().position - characterControl.transform.position;
        Quaternion q = Quaternion.FromToRotation(Vector3.up, dir);
        transform.rotation = q;

    }

    protected virtual Bullet SpawnBullet()
    {
        
        Bullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<Bullet>();
        b.transform.SetParent(GameManager.Instance.bulletSpawner);
        b.transform.position = shooter.transform.position;
        b.transform.rotation = shooter.transform.rotation;
        b.OnInits(this, butlletSpeed, transform.up);
        b.SetMaxHit(butlletMaxHit);
        b.SetSprite(bulletSprite);
        b.SetHitClip(hitClip);
        b.SetHitType(hitType);
        return b;
    }
}
