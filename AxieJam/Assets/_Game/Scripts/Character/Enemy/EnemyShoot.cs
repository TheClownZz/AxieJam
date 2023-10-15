using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyComponent, IAttack
{
    Player target;
    [SerializeField] Weapon currentWp;
    [SerializeField] CircleCollider2D col2d;

    Enemy eControl;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        eControl = (Enemy)enemy;
        currentWp.SetController(this);
        currentWp.OnInits(control);
        currentWp.SetTier(0);
        currentWp.SetDamageRate(eControl.waveStat.damageRate);
    }

    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
        if (target && !target.isDead)
        {
            currentWp.OnUpdate(dt);
        }
    }
    public Transform GetTarget()
    {
        return target ? target.transform : null;
    }

    public void OnWeaponUpdate()
    {
        col2d.radius = currentWp.stat.range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!target)
            target = collision.GetComponent<Player>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target && target == collision.GetComponent<Player>())
        {
            target = null;
        }
    }


}
