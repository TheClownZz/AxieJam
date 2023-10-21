using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, ICreateDamage
{
    protected Vector3 dir;
    protected float timePlaySound;
    protected float damageRate = 1;

    [SerializeField] protected AudioClip hitClip;
    [SerializeField] protected SpriteRenderer bulletRender;

    [HideInInspector] public Weapon weapon;

    public virtual void OnInits(Weapon weapon, float speed, Vector3 dir)
    {
        this.weapon = weapon;
        this.dir = dir * speed;

    }

    public void SetDamageRate(float value)
    {
        damageRate = value;
    }

    protected virtual void Update()
    {
        transform.position += dir * Time.deltaTime;

    }

    public virtual void Clear()
    {
        PoolManager.Instance.DespawnObject(transform);
    }
    protected virtual void HitCharacter(Character character)
    {
        CharacterStat stat = weapon.GetCharacterStat();
        float damage = stat.damage* damageRate;
        float critDamage = stat.critDamage;

        float critRate = stat.critRate;
        bool isCrit = Random.value <= critRate;
        if (isCrit)
        {
            damage += (damage * critDamage);
        }
        damage = Random.Range(0.9f * damage, 1.1f * damage);

        character.TakeDamage(damage, isCrit);
        character.KnockBack(dir.normalized, GameManager.Instance.gameConfig.forceValue);

        AddEffect(character);
        if (hitClip && Time.time - timePlaySound > 0.1f)
        {
            timePlaySound = Time.time;
            AudioManager.Instance.PlaySound(hitClip);
        }
    }
    public virtual void CreateDamage(Character character)
    {

        PreHit(character);
        HitCharacter(character);
        AfterHit(character);
        Clear();
    }

    protected virtual void PreHit(Character character)
    {

    }

    protected virtual void AfterHit(Character character)
    {

    }

    protected void AddEffect(Character character)
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

    public void SetSprite(Sprite sprite)
    {
        bulletRender.sprite = sprite;
    }

    public void SetHitClip(AudioClip clip)
    {
        hitClip = clip;
    }

    // only for enemy -- player do in player hp
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.GetComponent<Enemy>();
        if (!e || e.isDead)
            return;
        CreateDamage(e);
    }


}
