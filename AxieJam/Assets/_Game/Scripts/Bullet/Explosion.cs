
using UnityEngine;

public class Explosion : Bullet
{
    protected override void Update()
    {
    }
    public override void CreateDamage(Character character)
    {
        PreHit(character);
        HitCharacter(character);
        AfterHit(character);
    }
}
