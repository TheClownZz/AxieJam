using UnityEngine;

public class PierceBullet : Bullet
{
    public float rate;

    float cachedArmor;
    bool isRate;
    protected override void PreHit(Character character)
    {
        base.PreHit(character);
        isRate = Random.value <= rate;
        if (isRate)
        {
            cachedArmor = character.stat.armor;
            character.SetArmor(0);
        }
    }

    protected override void AfterHit(Character character)
    {
        base.AfterHit(character);
        if (isRate)
        {
            character.SetArmor(cachedArmor);
        }
    }
}
