using System.Collections.Generic;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    protected int tier;
    protected float attackTime;
    protected Character characterControl;

    [SerializeField] protected AudioClip attackClip;
    public List<EffectData> effectDataList;

    public virtual void OnInits(Character characterControl)
    {
        this.characterControl = characterControl;
        attackTime = 0;
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


    public virtual void LifeSteal(float hp)
    {
        characterControl.LifeSteal(hp);
    }
    public CharacterStat GetCharacterStat()
    {
        return characterControl.stat;
    }

}

