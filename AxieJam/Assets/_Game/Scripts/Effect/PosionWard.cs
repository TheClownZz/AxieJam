using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class PosionWard : HealingWard
{
    [SerializeField] Collider2D col2d;
    protected override void OnActive()
    {
        col2d.enabled = true;
        DOVirtual.DelayedCall(cooldown / 2, () =>
        {
            col2d.enabled = false;
        });
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakePosionDamage(activeValue);
        }
    }


}
