using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGun : PlayerGun
{
    [SerializeField] Transform exposionPrefab;
    Coroutine coroutine;
    public override void ActiveSKill(SkillConfig config)
    {
        base.ActiveSKill(config);
        float explosionRate = config.GetSkillValue(SkillType.Cooldown, 1f);
        float range = config.GetSkillValue(SkillType.Range, 2f);
        float size = config.GetSkillValue(SkillType.Size, 2f);
        float damage = config.GetSkillValue(SkillType.Damage, 1f);

        coroutine = StartCoroutine(ISpawnExplosion(explosionRate, range, size, damage));
    }


    IEnumerator ISpawnExplosion(float explosionRate, float range, float size, float damage)
    {
        while (true)
        {
            yield return new WaitForSeconds(explosionRate);
            Bullet b = PoolManager.Instance.SpawnObject(bulletPf.transform).GetComponent<Bullet>();
            b.transform.SetParent(GameManager.Instance.bulletSpawner, false);
            b.transform.position = characterControl.transform.position + (Vector3)Random.insideUnitCircle * Random.Range(0.8f * range, range);
            b.transform.rotation = Quaternion.identity;
            b.transform.localScale = Vector3.one * size;
            b.OnInits(this, 0, Vector3.zero);
            b.SetHitClip(hitClip);
            b.SetDamageRate(damage);
        }
    }
    public override void DeAvtiveSkill()
    {
        base.DeAvtiveSkill();
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
