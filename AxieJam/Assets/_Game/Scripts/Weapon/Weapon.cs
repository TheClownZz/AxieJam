using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    protected int tier;
    protected float attackTime;
    public Character characterControl;
    public WeaponConfig config;
    public WeaponStat stat;
    public List<EffectData> effectDataList;
    protected IAttack attckController;
    [SerializeField] protected AudioClip attackClip;

    protected float coolDown = 1;
    protected float range;
    [HideInInspector] public float damage;

    public virtual void OnInits(Character characterControl)
    {
        this.characterControl = characterControl;
        attackTime = 0;

        transform.SetParent(characterControl.body);
        transform.position = characterControl.transform.position;

    }

    public void SetDamageRate(float rate)
    {
        damage = stat.damage * rate;

    }
    public virtual void SetTier(int tier)
    {
        this.tier = tier;
        stat = config.GetStat(tier);

        float _as = characterControl.stat.attackSpeed * (1 + stat.attackSpeed);
        coolDown = 1f / _as;
        attckController.OnWeaponUpdate();
    }

    public virtual void OnUpdate(float dt)
    {
        FaceToTarget(dt);
        if (CheckCoolDown() && CheckRange())
        {
            OnAttack();
        }
    }

    public virtual void OnAttack()
    {
        attackTime = Time.time;
        if (attackClip)
        {
            AudioManager.Instance.PlaySound(attackClip);
        }
    }
    public virtual void OnClear()
    {
        PoolManager.Instance.DespawnObject(transform);
    }



    protected virtual void FaceToTarget(float dt)
    {

    }

    public virtual void LifeSteal(float hp)
    {
        characterControl.LifeSteal(hp);
    }
    public CharacterStat GetCharacterStat()
    {
        return characterControl.stat;
    }

    public void SetController(IAttack ctl)
    {
        attckController = ctl;
    }

    private bool CheckCoolDown()
    {
        return Time.time - attackTime > coolDown;
    }

    private bool CheckRange()
    {
        if (!attckController.GetTarget())
            return false;
        return Vector2.Distance(transform.position, attckController.GetTarget().position) < stat.range;
    }
}

