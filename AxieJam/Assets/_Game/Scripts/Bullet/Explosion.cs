
public class Explosion : Bullet
{
    public override void CreateDamage(Character character)
    {
        PreHit(character);
        HitCharacter(character);
        AfterHit(character);
    }
}
