
using UnityEngine;

public class Explosion : Bullet
{
    [SerializeField] AudioGetter audioGetter;

    private void OnEnable()
    {
        AudioManager.Instance.PlaySound(audioGetter.clip);
    }
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
