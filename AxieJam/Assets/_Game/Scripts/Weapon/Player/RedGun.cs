using System.Collections;
using UnityEngine;

public class RedGun : PlayerGun
{
    [SerializeField] Transform exposionPrefab;
    Coroutine coroutine;
    public override void ActiveSKill(SkillConfig config)
    {
        base.ActiveSKill(config);
        int number = (int)config.GetSkillValue(SkillType.Number, 1);
        float explosionRate = config.GetSkillValue(SkillType.Cooldown, 1f);
        float range = config.GetSkillValue(SkillType.Range, 2f);
        float size = config.GetSkillValue(SkillType.Size, 2f);
        float damage = config.GetSkillValue(SkillType.Damage, 1f);

        coroutine = StartCoroutine(ISpawnExplosion(explosionRate, range, size, damage, number));
    }


    IEnumerator ISpawnExplosion(float explosionRate, float range, float size, float damage, int number)
    {
        while (true)
        {
            yield return new WaitForSeconds(explosionRate);
            for (int i = 0; i < number; i++)
            {
                Bullet bullet = PoolManager.Instance.SpawnObject(exposionPrefab).GetComponent<Bullet>();
                bullet.transform.SetParent(GameManager.Instance.bulletSpawner, false);
                bullet.transform.position = characterControl.transform.position + (Vector3)Random.insideUnitCircle * Random.Range(0.8f * range, range);
                bullet.transform.rotation = Quaternion.identity;
                bullet.transform.localScale = Vector3.one * size;
                bullet.OnInits(this, 0, Vector3.zero);
                bullet.SetHitClip(hitClip);
                bullet.SetDamageRate(damage);
                GameManager.Instance.DelayedCall(0.2f, () =>
                {
                    bullet.SetCol(false);
                });

                GameManager.Instance.DelayedCall(1f, () =>
                {
                    if (bullet.gameObject.activeInHierarchy)
                        PoolManager.Instance.DespawnObject(bullet.transform);
                });

            }
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
