
using UnityEngine;
using DG.Tweening;

public class EnemyAttack : EnemyComponent
{
    protected float timeAttack;
    protected float coolDown;
    [HideInInspector] public Player target;

    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        coolDown = 1f / control.stat.attackSpeed;
        timeAttack = Time.time;

    }


    public override void OnUpdate(float dt)
    {
        if (!control) return;
        base.OnUpdate(dt);

        if (target && Time.time - timeAttack >= coolDown)
        {
            OnAttack();
        }
    }
    public virtual void OnAttack()
    {
        timeAttack = Time.time;
        Attacktarget();
    }

    public virtual void OnAttackDone()
    {
        control.DisableEnemy(false);
    }
    public virtual void Attacktarget()
    {
        if (!target) return;

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
    public override void OnDead()
    {
        base.OnDead();
        target = null;
    }

    protected virtual void SetTarget(Player target)
    {
        this.target = target;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!target)
            SetTarget(collision.GetComponent<Player>());
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (target && target == collision.GetComponent<Player>())
        {
            SetTarget(null);
        }
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
