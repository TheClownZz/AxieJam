using DG.Tweening;
using UnityEngine;

public class BossJumpAttack : BossAttack
{
    [SerializeField] AudioClip clip;
    [SerializeField] float jumpPower;
    [SerializeField] float jumptime;
    [SerializeField] float attackAoe = 2;
    [SerializeField] ParticleSystem fxSlam;

    float damageRate;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        BossSkillConfig config = control.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        coolDown = config.GetSkillValue(SkillType.Cooldown, 1);
        damageRate = config.GetSkillValue(SkillType.Damage, 1);
        transform.localScale = Vector3.one * (config.GetSkillValue(SkillType.Range, 10));
    }
    public override void OnAttack()
    {
        if (control.GetCom<BossMove>().isJump) return;

        timeAttack = Time.time;
        control.DisableEnemy(true);
        control.SetState(CharacterState.Attack);
        control.transform.DOJump(target.transform.position, jumpPower, 1, jumptime);
    }

    public override void Attacktarget()
    {
        fxSlam.Play();
        AudioManager.Instance.PlaySound(clip);

        GameManager.Instance.DelayedCall(0.1f, () =>
        {
            Player player = GameManager.Instance.GetCurrentPlayer();
            if (Vector3.Distance(transform.position, player.transform.position) <= attackAoe)
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



    }
}
