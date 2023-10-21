using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletThrough : Bullet
{
    public override void CreateDamage(Character character)
    {
        PreHit(character);
        HitCharacter(character);
        AfterHit(character);
    }
}
