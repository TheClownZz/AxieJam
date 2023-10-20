using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemykamikazeAttack : EnemyAttack
{
    const float offset = 0.1f;
    [SerializeField] CircleCollider2D circleCollider;

    float cachedRadius;
    private void Awake()
    {
        cachedRadius = circleCollider.radius;

    }
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        circleCollider.radius = cachedRadius;
    }

    public override void OnAttack()
    {
        circleCollider.radius = cachedRadius + offset;
        control.SetState(CharacterState.Attack);
        control.DisableEnemy(true);
    }

    public override void OnAttackDone()
    {
        base.OnAttackDone();
        control.OnDead();
    }
}
