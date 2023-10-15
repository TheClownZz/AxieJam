using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyAttack : EnemyComponent
{
    float timeAttack;
    float coolDown;
    Player target;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        timeAttack = 0;
        coolDown = 1f / control.stat.attackSpeed;
    }


    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
        if (target && Time.time - timeAttack >= coolDown && !control.isDisable)
        {
            OnAttack();
        }
    }
    public override void OnDead()
    {
        base.OnDead();
        target = null;
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

    public virtual void OnAttack()
    {
        timeAttack = Time.time;

        float dodge = target.stat.dodge;

        if (Random.value <= dodge)
        {
            SpawnText();
            return;

        }

        float damage = control.stat.damage;
        bool isCrit = Random.value <= control.stat.critRate;
        if (isCrit)
            damage += damage * control.stat.critDamage;
        target.GetPCom<PlayerHp>().TakeDamage(damage, isCrit);
    }


    private void SpawnText()
    {
        float height = GameManager.Instance.gameConfig.textHeight;
        var dd = PoolManager.Instance.SpawnObject(PoolType.TextDisplay).GetComponent<TextDisplay>();
        dd.ShowMiss();
        //  dd.transform.SetParent(GameManager.Instance.gameConfig.textParent);
        dd.transform.position = transform.position + height * Vector3.up;
        dd.transform.DOMoveY(dd.transform.position.y + height, 0.5f).OnComplete(() =>
        {
            PoolManager.Instance.DespawnObject(dd.transform);
        });
    }
}
