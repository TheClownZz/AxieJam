using UnityEngine;

public class BlueGun : PlayerGun
{
    float angleRate = 10;
    int numberBullet = 1;
    protected override Bullet SpawnBullet()
    {
        int offset = (numberBullet + 1) / 2;
        for (int i = 1; i <= numberBullet; i++)
        {
            float angle = angleRate * (i - offset);
            LightingBullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<LightingBullet>();
            b.transform.SetParent(GameManager.Instance.bulletSpawner, false);
            b.transform.position = shooter.transform.position;
            b.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);
            b.OnInits(this, butlletSpeed, -b.transform.right);
            b.SetSprite(bulletSprite);
            b.SetHitClip(hitClip);
            b.SetColor(color);
            b.SetDamageRate(damageRate);

        }
        return null;

    }

    public override void ActiveSKill(SkillConfig config)
    {
        base.ActiveSKill(config);
        damageRate *= config.GetSkillValue(SkillType.Damage, 1f);
        numberBullet = (int)config.GetSkillValue(SkillType.Number, 1);
    }

    public override void DeAvtiveSkill()
    {
        base.DeAvtiveSkill();
        numberBullet = 1;
    }
}
