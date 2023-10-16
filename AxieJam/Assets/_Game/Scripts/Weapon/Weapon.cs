using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    protected int tier;
    protected float attackTime;
    public Character characterControl;
    public List<EffectData> effectDataList;
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
        //damage = stat.damage * rate;

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

    private bool CheckCoolDown()
    {
        return Time.time - attackTime > coolDown;
    }

    private bool CheckRange()
    {
        return false;
    }
}

