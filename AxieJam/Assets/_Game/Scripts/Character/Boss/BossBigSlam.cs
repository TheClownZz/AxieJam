using DG.Tweening;
using Game;
using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BossBigSlam : EnemyAttack
{
    [SerializeField] Weapon weapon;
    [SerializeField] int skillIndex = 1;
    [SerializeField] float attackSize = 2;

    [SerializeField] ParticleSystem fxSlam;
    [SerializeField] Transform furry;
    [SerializeField] Transform exposionPrefab;
    [SerializeField] AudioClip attackClip;
    [SerializeField] AudioClip explosionClip;
    List<Transform> spawnList = new List<Transform>();

    int numberExplosion;
    float damageRate;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        weapon.OnInits(enemy);

        BossSkillConfig config = control.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        coolDown = config.GetSkillValue(SkillType.Cooldown, 3);
        damageRate = config.GetSkillValue(SkillType.Damage, 1);
        attackSize = config.GetSkillValue(SkillType.Range, 2);
        numberExplosion = (int)config.GetSkillValue(SkillType.Number, 3);

        furry.transform.localScale = attackSize * Vector3.one;
        fxSlam.transform.SetParent(GameManager.Instance.GetMapTf().GetChild(1));

    }
    public override void OnAttack()
    {
        if (control.GetCom<BossMove>().isJump) return;
        timeAttack = Time.time;
        control.DisableEnemy(true);
        control.SetState(CharacterState.Attack);
    }

    public override void Attacktarget()
    {
        spawnList.Clear();
        fxSlam.Play();
        fxSlam.transform.position = furry.transform.position;

        AudioManager.Instance.PlaySound(attackClip);
        GameManager.Instance.DelayedCall(0.1f, () =>
        {
            Player player = GameManager.Instance.currentPlayer;
            if (Vector3.Distance(furry.transform.position, player.transform.position) <= attackSize)
            {
                float dodge = player.stat.dodge;

                if (Random.value <= dodge)
                {
                    SpawnText();
                    return;
                }
                float damage = control.stat.damage * damageRate;
                bool isCrit = Random.value <= control.stat.critRate;
                if (isCrit)
                    damage += damage * control.stat.critDamage;
                player.GetPCom<PlayerHp>().TakeDamage(damage, isCrit);
            }

        });

        float delay = 0.1f;
        float delayWarning = 0.5f;
        for (int i = 0; i < numberExplosion; i++)
        {
            Vector3 spawnPos = GameManager.Instance.levelController.GetSpawnErea();
            var spawnFx = PoolManager.Instance.SpawnObject(PoolType.SpawnFx);
            spawnFx.position = spawnPos;
            spawnList.Add(spawnFx);
        }

        for (int i = 0; i < spawnList.Count; i++)
        {
            int index = i;
            GameManager.Instance.DelayedCall(delayWarning + delay * i, () =>
            {

                AudioManager.Instance.PlaySound(explosionClip);
                PoolManager.Instance.DespawnObject(spawnList[index]);
                Bullet bullet = PoolManager.Instance.SpawnObject(exposionPrefab).GetComponent<Bullet>();
                bullet.transform.SetParent(GameManager.Instance.bulletSpawner, false);
                bullet.transform.position = spawnList[index].transform.position;
                bullet.transform.rotation = Quaternion.Euler(-30, 0, 0);
                bullet.OnInits(weapon, 0, Vector3.zero);
                bullet.SetDamageRate(damageRate);
                GameManager.Instance.DelayedCall(0.2f, () =>
                {
                    bullet.SetCol(false);
                });

                GameManager.Instance.DelayedCall(1f, () =>
                {
                    if (bullet.gameObject.activeInHierarchy)
                        PoolManager.Instance.DespawnObject(bullet.transform);
                });
            });
        }
    }
}
