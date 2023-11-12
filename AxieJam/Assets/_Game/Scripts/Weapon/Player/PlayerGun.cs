using UnityEngine;

public class PlayerGun : Weapon
{
    [SerializeField] protected Bullet bulletPf;
    [SerializeField] protected Transform shooter;
    [SerializeField] protected float butlletSpeed = 22f;
    [SerializeField] protected Sprite bulletSprite;
    [SerializeField] protected float force = 300;
    [SerializeField] GameObject gunFx;

    [SerializeField] AudioGetter attackClipGetter;

    protected float damageRate = 1;
    protected float cachedDamageRate;

    protected bool isActive = false;
    CameraShake cameraShake;

    public override void OnInits(Character characterControl)
    {
        base.OnInits(characterControl);
        cameraShake = Camera.main.GetComponent<CameraShake>();
        cachedDamageRate = damageRate;

        attackClip = attackClipGetter.clip;
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
        b.OnInits(this, butlletSpeed, -b.transform.right);
        b.SetDamageRate(damageRate);
        return b;
    }

    public virtual void ActiveSKill(SkillConfig config)
    {
        isActive = true;
        cachedDamageRate = damageRate;
        GameManager.Instance.DelayedCall(config.defaultValue.duration, DeAvtiveSkill);
    }

    public virtual void DeAvtiveSkill()
    {
        isActive = false;
        damageRate = cachedDamageRate;
    }

    public override void OnSelect()
    {
        base.OnSelect();
        gunFx.SetActive(false);
        GameManager.Instance.DelayedCall(0.1f, () =>
        {
            gunFx.SetActive(true);
        });
    }
}
