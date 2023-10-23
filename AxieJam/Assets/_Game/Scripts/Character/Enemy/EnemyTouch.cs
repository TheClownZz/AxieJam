using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTouch : EnemyComponent
{
    const float damgeRate = 3;
    const string touched = "attack/ranged/goo-destruct";

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p)
        {
            control.spineController.SetDieAnim(touched);
            control.OnDead();
            AttackPlayer(p);
        }
    }

    protected void AttackPlayer(Player p)
    {
        float dodge = p.stat.dodge;

        if (Random.value <= dodge)
        {
            SpawnText();
            return;

        }
        float damage = control.stat.damage * damgeRate;
        bool isCrit = Random.value <= control.stat.critRate;
        if (isCrit)
            damage += damage * control.stat.critDamage;
        p.GetPCom<PlayerHp>().TakeDamage(damage, isCrit);
    }

    protected void SpawnText()
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
