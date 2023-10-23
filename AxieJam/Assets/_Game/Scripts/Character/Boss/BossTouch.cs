using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTouch : EnemyTouch
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.GetComponent<Player>();
        if (p)
        {
            AttackPlayer(p);
        }
    }
}
