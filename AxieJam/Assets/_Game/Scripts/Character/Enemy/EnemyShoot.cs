using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyComponent
{
    Player target;
    [SerializeField] Weapon currentWp;
    [SerializeField] CircleCollider2D col2d;

    Enemy eControl;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        eControl = (Enemy)enemy;
        currentWp.OnInits(control);
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
