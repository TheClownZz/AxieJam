using UnityEngine;
using DG.Tweening;

public class EnemyHp : EnemyComponent, ITakeDamage
{
    public float currentHp;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        currentHp = control.stat.hp;
    }

    public virtual float TakeDamage(float damage, bool isCrit)
    {
        if (damage == 0 || control.isDead)
            return 0;
        float damageRate = damage / (damage + control.stat.armor);
        damage = damage * damageRate;
        damage = Mathf.Max(damage, 1);
        ShowDamageText(damage, isCrit);
        currentHp -= damage;
        if (currentHp <= 0)
        {
            control.OnDead();
        }
        return damage;
    }

    // for optimize move check enemy hit to butllet

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (control.isDead)
    //         return;
    //     ICreateDamage hitter = collision.GetComponent<ICreateDamage>();
    //     if (hitter != null)
    //     {
    //         hitter.CreateDamage(control);
    //     }
    // }

    private void ShowDamageText(float damage, bool isCrit)
    {
        float height = GameManager.Instance.gameConfig.textHeight;
        var dd = PoolManager.Instance.SpawnObject(PoolType.TextDisplay).GetComponent<TextDisplay>();
        dd.ShowDamage(damage, isCrit);
        // dd.transform.SetParent(GameManager.Instance.gameConfig.textParent);
        dd.transform.position = transform.position + height * Vector3.up;
        dd.transform.DOMoveY(dd.transform.position.y + height, 0.5f).OnComplete(() =>
        {
            PoolManager.Instance.DespawnObject(dd.transform);
        });
    }
}
