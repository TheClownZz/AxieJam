using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour, ICreateDamage
{
    public Weapon weapon;

    protected float damageDeal;

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public virtual void OnInits(Weapon weapon)
    {
        this.weapon = weapon;
    }

    protected virtual void HitCharacter(Character character)
    {
        CharacterStat stat = weapon.GetCharacterStat();
        float damage = stat.damage + weapon.damage;
        float critDamage = stat.critDamage;

        float critRate = stat.critRate;
        bool isCrit = Random.value <= critRate;
        if (isCrit)
        {
            damage += (damage * critDamage);
        }

        damage = Random.Range(0.9f * damage, 1.1f * damage);

        damageDeal = character.TakeDamage(damage, isCrit);

        weapon.LifeSteal(damageDeal * weapon.characterControl.stat.lifeSteal);

        Vector2 dir = character.transform.position - transform.position;
        character.KnockBack(dir.normalized, GameManager.Instance.gameConfig.forceValue);

        AddEffect(character);
    }

    public void CreateDamage(Character character)
    {
        HitCharacter(character);
    }

    private void AddEffect(Character character)
    {
        EffectController controller = character.GetCom<EffectController>();
        if (!controller)
            return;
        foreach (var data in weapon.effectDataList)
        {
            bool isActive = Random.value <= data.rate;
            if (!isActive)
                continue;
            switch (data.effectType)
            {
                case EffectType.Stun:
                    StunEffect effect = new StunEffect(data.duration);
                    controller.AddEffect(effect);
                    break;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            CreateDamage(enemy);
        }
    }
}
