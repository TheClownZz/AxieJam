using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeGun : PlayerGun
{
    const float animTime = 0.5f;
    [SerializeField] List<float> speedList;
    [SerializeField] List<float> radiusList;
    [SerializeField] List<Transform> circleList;
    [SerializeField] List<Bullet> bulletList;
    [SerializeField] Transform circleBulletPrefab;
    Coroutine coroutine;
    public override void ActiveSKill(SkillConfig config)
    {
        base.ActiveSKill(config);


        int numberBullet = (int)config.GetSkillValue(SkillType.Number, 0);
        int numberExtraBullet = (int)config.GetSkillValue(SkillType.ExtraNumber, 0);
        float damageRate = config.GetSkillValue(SkillType.Damage, 1f);
        float extraDamgeRate = config.GetSkillValue(SkillType.ExtraDamage, 1f);

        if (numberBullet > 0)
        {
            float angleRate = 360 / numberBullet;
            for (int i = 0; i < numberBullet; i++)
            {
                float angle = angleRate * i;
                Bullet b = PoolManager.Instance.SpawnObject(circleBulletPrefab).GetComponent<Bullet>();
                b.transform.position = FrameWorkUtility.SpawnInCircle(circleList[0].position, radiusList[0], angle);
                b.transform.rotation = Quaternion.Euler(0, 0, 360 - angle);
                b.transform.localScale = Vector3.one;
                b.transform.SetParent(circleList[0], true);
                b.OnInits(this, 0, Vector3.zero);
                b.SetSprite(bulletSprite);
                b.SetHitClip(hitClip);
                b.SetDamageRate(damageRate);
                bulletList.Add(b);
            }
        }

        if (numberExtraBullet > 0)
        {
            float angleRate = 360 / numberExtraBullet;
            for (int i = 0; i < numberExtraBullet; i++)
            {
                float angle = angleRate * (i + 1);
                Bullet b = PoolManager.Instance.SpawnObject(circleBulletPrefab).GetComponent<Bullet>();
                b.transform.position = FrameWorkUtility.SpawnInCircle(circleList[1].position, radiusList[1], angle);
                b.transform.rotation = Quaternion.Euler(0, 0, 360 - angle);
                b.transform.localScale = Vector3.one;
                b.transform.SetParent(circleList[1], true);
                b.OnInits(this, 0, Vector3.zero);
                b.SetSprite(bulletSprite);
                b.SetHitClip(hitClip);
                b.SetDamageRate(extraDamgeRate);
                bulletList.Add(b);
            }
        }
        coroutine = StartCoroutine(IUpdateCircle());
        foreach (Transform circle in circleList)
        {
            circle.localScale = Vector3.zero;
            circle.gameObject.SetActive(true);
            circle.DOScale(Vector3.one, animTime);
        }

    }

    public override void DeAvtiveSkill()
    {
        base.DeAvtiveSkill();
        if (coroutine != null)
            StopCoroutine(coroutine);
        foreach (Transform circle in circleList)
        {
            circle.DOScale(Vector3.zero, animTime).OnComplete(() =>
            {
                circle.localScale = Vector3.one;
                circle.gameObject.SetActive(false);
                foreach (var bullet in bulletList)
                {
                    bullet.Clear();
                }
                bulletList.Clear();
            });

        }
    }

    IEnumerator IUpdateCircle()
    {
        while (true)
        {
            for (int i = 0; i < circleList.Count; i++)
            {
                circleList[i].Rotate(0, 0, speedList[i] * Time.deltaTime);
            }
            yield return null;
        }
    }


}
