using System.Collections.Generic;
using UnityEngine;

public class BossBigSlam : BossAttack
{
    [SerializeField] protected Weapon weapon;

    [SerializeField] protected ParticleSystem fxSlam;
    [SerializeField] protected Transform wpTf;
    [SerializeField] protected Transform exposionPrefab;
    [SerializeField] protected AssetGetter explosionClipGetter;

    [SerializeField] protected float radius = 3f;
    [SerializeField] float explosionTime = 1f;
    protected float attackSize = 2;
    protected float damageRate;
    protected int numberExplosion;
    protected AudioClip explosionClip;
    protected List<Transform> spawnList = new List<Transform>();

    protected override void Awake()
    {
        base.Awake();
        explosionClipGetter.OnGetAsset = (audio) =>
        {
            explosionClip = (AudioClip)audio;
        };
        explosionClipGetter.LoadAsset();
    }
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        weapon.OnInits(enemy);

        BossSkillConfig config = control.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        coolDown = config.GetSkillValue(SkillType.Cooldown, 3);
        damageRate = config.GetSkillValue(SkillType.Damage, 1);
        attackSize = config.GetSkillValue(SkillType.Range, 2);
        numberExplosion = (int)config.GetSkillValue(SkillType.Number, 3);

        wpTf.transform.localScale = attackSize * Vector3.one;
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
        fxSlam.transform.position = wpTf.transform.position;

        if (attackClip)
            AudioManager.Instance.PlaySound(attackClip);
        GameManager.Instance.DelayedCall(0.1f, () =>
        {
            Player player = GameManager.Instance.GetCurrentPlayer();
            if (Vector3.Distance(wpTf.transform.position, player.transform.position) <= attackSize)
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
            Vector3 spawnPos = GetSpawnPos();
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

                GameManager.Instance.DelayedCall(explosionTime, () =>
                {
                    if (bullet.gameObject.activeInHierarchy)
                        PoolManager.Instance.DespawnObject(bullet.transform);
                });
            });
        }
    }

    public virtual Vector3 GetSpawnPos()
    {
        return FrameWorkUtility.SpawnInCircle(control.transform.position, radius, Random.Range(0, 359));
    }
}
