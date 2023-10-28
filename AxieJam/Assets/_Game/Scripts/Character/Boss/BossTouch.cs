
using UnityEngine;

public class BossTouch : EnemyTouch
{
    [SerializeField] float attackRate = 0.5f;
    float attackTime;
    private void OnEnable()
    {
        attackTime = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p && !control.isDead && p.GetCom<PlayerHp>().allowTakeDamge && Time.time - attackTime > attackRate)
        {
            attackTime = Time.time;
            AttackPlayer(p);
        }
    }



}
