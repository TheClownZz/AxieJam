using UnityEngine;

public class EnemyFlowerGun : EnemyGun
{
    float angleRate = 10;
    int numberBullet = 3;

    public override Bullet SpawnBullet()
    {
        Vector3 dir = (targetPos - shooter.transform.position).normalized;
        int offset = (numberBullet - 1) / 2;
        for (int i = 1; i <= numberBullet; i++)
        {

            float angle = angleRate * (i - offset);
            Vector3 _dir = Quaternion.AngleAxis(angle, Vector3.forward) * dir;

            EnemyBullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<EnemyBullet>();
            b.transform.SetParent(GameManager.Instance.bulletSpawner, false);
            b.transform.position = shooter.transform.position;
            b.OnInits(this, butlletSpeed, _dir);
            b.SetSprite(bulletSprite);
            b.SetDamageRate(1);
        }
        return null;

    }
}
