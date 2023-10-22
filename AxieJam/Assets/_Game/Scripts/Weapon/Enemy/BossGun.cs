using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : EnemyGun
{
    [SerializeField] int skillIndex = 0;

    float damageRate = 1;
    float angleRate = 10;
    int numberBullet = 1;
    public override Bullet SpawnBullet()
    {
        int offset = (numberBullet - 1) / 2;
        for (int i = 1; i <= numberBullet; i++)
        {
            float angle = angleRate * (i - offset);
            Bullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<Bullet>();
            b.transform.SetParent(GameManager.Instance.bulletSpawner, false);
            b.transform.position = shooter.transform.position;
            b.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);
            b.OnInits(this, butlletSpeed, -b.transform.right);
            b.SetSprite(bulletSprite);
            b.SetDamageRate(damageRate);
        }
        return null;

    }
    protected override void UpdateStat()
    {
        BossSkillConfig config = characterControl.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        damageRate = config.GetSkillValue(SkillType.Damage, 1);
        numberBullet = (int)config.GetSkillValue(SkillType.NumberBullet, 1);
        transform.localScale = Vector3.one * (config.GetSkillValue(SkillType.Range, 10));
    }
}
