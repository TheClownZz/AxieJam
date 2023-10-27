using UnityEngine;

public class EnemyShoot : EnemyAttack
{
    [SerializeField] EnemyGun gun;

    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        gun.OnInits(enemy);
    }

    public override void OnAttack()
    {
        timeAttack = Time.time;
        control.DisableEnemy(true);
        control.SetState(CharacterState.Attack);
        gun.targetPos = target.transform.position;
    }

    protected override void SetTarget(Player target)
    {
        if(this.target && !target)
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
