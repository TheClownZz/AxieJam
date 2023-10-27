using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyAttack
{
    protected int skillIndex;
    [SerializeField] protected AudioClip attackClip;
    public void SetIndex(int index)
    {
        this.skillIndex = index;
    }
}
