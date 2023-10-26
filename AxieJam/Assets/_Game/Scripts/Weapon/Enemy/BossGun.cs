using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : EnemyGun
{
    [SerializeField] int skillIndex = 0;
    [SerializeField] int numberBullet = 1;

    float damageRate = 1;
    float angleRate = 10;

    public override void OnInits(Character characterControl)
    {
        base.OnInits(characterControl);
        UpdateStat();
    }
    public override Bullet SpawnBullet()
    {
        Vector3 dir = (targetPos - shooter.transform.position).normalized;
        int offset = (numberBullet - 1) / 2;
        for (int i = 1; i <= numberBullet; i++)
        {

            float angle = angleRate * (i - offset);
            Vector3 _dir = Quaternion.AngleAxis(angle, Vector3.forward) * dir;

            Bullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<Bullet>();
            b.transform.SetParent(GameManager.Instance.bulletSpawner, false);
            b.transform.position = shooter.transform.position;
            b.OnInits(this, butlletSpeed, _dir);
            b.SetSprite(bulletSprite);
            b.SetDamageRate(damageRate);
        }
        return null;

    }
    protected void UpdateStat()
    {
        BossSkillConfig config = characterControl.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        damageRate = config.GetSkillValue(SkillType.Damage, 1);
        numberBullet = (int)config.GetSkillValue(SkillType.Number, 1);
        transform.localScale = Vector3.one * (config.GetSkillValue(SkillType.Range, 10));
    }
}
