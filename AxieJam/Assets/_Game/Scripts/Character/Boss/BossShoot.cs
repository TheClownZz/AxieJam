using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossShoot : BossAttack
{
    [SerializeField] EnemyGun gun;

    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        gun.OnInits(enemy);
        BossSkillConfig config = control.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        coolDown = config.GetSkillValue(SkillType.Cooldown, 1);
    }

    public override void OnAttack()
    {
        timeAttack = Time.time;
        control.DisableEnemy(true);
        control.SetState(CharacterState.Attack);
        gun.targetPos = target.transform.position;
        if (attackClip)
            AudioManager.Instance.PlaySound(attackClip);
    }

    protected override void SetTarget(Player target)
    {
        if (this.target && !target)
            gun.targetPos = this.target.transform.position;

        base.SetTarget(target);
    }
    public override void Attacktarget()
    {
        if (target)
            gun.targetPos = target.transform.position;
        gun.SpawnBullet();
    }
}
