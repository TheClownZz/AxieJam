using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : Weapon
{
    public override void SetTier(int tier)
    {
        this.tier = tier;
        stat = config.GetStat(tier);

        float _as = characterControl.stat.attackSpeed * (1 + stat.attackSpeed);
        coolDown = 1f / _as;
    }
}
