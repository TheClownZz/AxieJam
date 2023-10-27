using DG.Tweening;
using UnityEngine;

public class BossSurfingAttack : BossAttack
{
    [SerializeField] string buffAnim;
    [SerializeField] float moveSpeed =1f;
    [SerializeField] float attackSize = 2;
    [SerializeField] AudioClip clip;
    [SerializeField] ParticleSystem fxSlam;
    [SerializeField] Transform furry;
    float damageRate;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        BossSkillConfig config = control.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        coolDown = config.GetSkillValue(SkillType.Cooldown, 3);
        damageRate = config.GetSkillValue(SkillType.Damage, 1);
        attackSize = config.GetSkillValue(SkillType.Range, 2);
        furry.transform.localScale = attackSize * Vector3.one;
        fxSlam.transform.SetParent(GameManager.Instance.GetMapTf().GetChild(1));

    }
    public override void OnAttack()
    {
        if (control.GetCom<BossMove>().isJump) return;
        timeAttack = Time.time;
        control.DisableEnemy(true);
        control.spineController.SetAnim(buffAnim, true);
        control.transform.DOMove(target.transform.position, moveSpeed)
            .SetDelay(0.3f)
            .SetSpeedBased(true)
            .SetEase(Ease.Linear).OnComplete(() =>
        {
            control.SetState(CharacterState.Attack);

        });
    }

    public override void Attacktarget()
    {
        fxSlam.Play();
        fxSlam.transform.position = furry.transform.position;

        AudioManager.Instance.PlaySound(clip);
        GameManager.Instance.DelayedCall(0.1f, () =>
        {
            Player player = GameManager.Instance.GetCurrentPlayer();
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



    }
}
