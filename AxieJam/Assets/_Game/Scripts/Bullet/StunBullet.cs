using UnityEngine;

public class StunBullet : Bullet
{
    [SerializeField] float duration = 1f;
    protected override void HitCharacter(Character character)
    {
        base.HitCharacter(character);
        StunEffect ef = new StunEffect(duration);
        character.GetCom<EffectController>().AddEffect(ef);
    }

}
