using DG.Tweening;
using UnityEngine;

public class PlayerGun : Weapon
{
    [SerializeField] protected Bullet bulletPf;
    [SerializeField] protected Transform shooter;
    [SerializeField] protected float butlletSpeed = 22f;
    [SerializeField] Sprite bulletSprite;
    [SerializeField] AudioClip hitClip;
    [SerializeField] float force = 300;

    CameraShake cameraShake;

    protected float damageRate = 1;

    protected bool isActive = false;
    public override void OnInits(Character characterControl)
    {
        base.OnInits(characterControl);
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }
    public override void OnAttack()
    {
        base.OnAttack();
        SpawnBullet();
        cameraShake.SmallShake();
        characterControl.GetCom<PlayerMove>().ForceBack(force * transform.right);
    }

    protected virtual Bullet SpawnBullet()
    {
        Bullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<Bullet>();
        b.transform.SetParent(GameManager.Instance.bulletSpawner, false);
        b.transform.position = shooter.transform.position;
        b.transform.rotation = shooter.transform.rotation;
        b.OnInits(this, butlletSpeed, -transform.right);
        b.SetSprite(bulletSprite);
        b.SetHitClip(hitClip);
        b.SetDamageRate(damageRate);
        return b;
    }

    public virtual void ActiveSKill(PlayerSkillConfig config)
    {
        isActive = true;
        DOVirtual.DelayedCall(config.defaultValue.duration, DeAvtiveSkill);
    }

    public virtual void DeAvtiveSkill()
    {
        isActive = false;
    }
}
