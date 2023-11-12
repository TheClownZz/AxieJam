using UnityEngine;

public class EnemyGun : Weapon
{
    [HideInInspector] public Vector3 targetPos;
    [SerializeField] protected Bullet bulletPf;
    [SerializeField] protected Transform shooter;
    [SerializeField] protected float butlletSpeed = 10f;
    [SerializeField] protected Sprite bulletSprite;

    public override void OnAttack()
    {
        base.OnAttack();
        SpawnBullet();
    }
    public virtual Bullet SpawnBullet()
    {
        Vector3 dir = (targetPos - shooter.transform.position).normalized;
        EnemyBullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<EnemyBullet>();
        b.transform.SetParent(GameManager.Instance.bulletSpawner);
        b.transform.position = shooter.transform.position;
        b.transform.rotation = shooter.transform.rotation;
        b.OnInits(this, butlletSpeed, dir);
        b.SetSprite(bulletSprite);
        b.SetDamageRate(1);
        return b;
    }
}
