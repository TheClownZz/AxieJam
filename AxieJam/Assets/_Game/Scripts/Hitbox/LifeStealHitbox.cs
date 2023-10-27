using UnityEngine;

public class LifeStealHitbox : Hitbox
{
    [SerializeField] float rate = 0.18f;
    [SerializeField] float lifeSteal = 1f;

    protected override void HitCharacter(Character character)
    {
        base.HitCharacter(character);
        bool isLifeSteal = Random.value <= rate;
        if (isLifeSteal)
        {
            weapon.LifeSteal(damageDeal);
        }
    }
}
