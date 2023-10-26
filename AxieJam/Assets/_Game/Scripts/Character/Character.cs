using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    None,
    Alive,
    Idle,
    Run,
    Die,
    Attack,
    Hit,
}

[System.Serializable]
public class CharacterStat
{
    public float hp;
    public float regen;
    public float armor;
    public float damage;
    public float critRate;
    public float critDamage;
    public float attackSpeed;
    public float moveSpeed;
    public float lifeSteal;
    public float dodge;

    public void Copy(CharacterStat stat)
    {
        SetHp(stat.hp).SetRegen(stat.regen).Setarmor(stat.armor).
            SetDamage(stat.damage).SetCritRate(stat.critRate).
            SetAttackSpeed(stat.attackSpeed).SetMoveSpeed(stat.moveSpeed).
            SetLifeSteal(stat.lifeSteal).SetDodge(stat.dodge);
    }

    public CharacterStat SetHp(float value)
    {
        hp = value;
        return this;
    }

    public CharacterStat SetRegen(float value)
    {
        regen = value;
        return this;
    }

    public CharacterStat Setarmor(float value)
    {
        armor = value;
        return this;
    }

    public CharacterStat SetDamage(float value)
    {
        damage = value;
        return this;
    }

    public CharacterStat SetCritRate(float value)
    {
        critRate = value;
        return this;
    }

    public CharacterStat SetCritDamage(float value)
    {
        critDamage = value;
        return this;
    }

    public CharacterStat SetAttackSpeed(float value)
    {
        attackSpeed = value;
        return this;
    }

    public CharacterStat SetMoveSpeed(float value)
    {
        moveSpeed = value;
        return this;
    }

    public CharacterStat SetLifeSteal(float value)
    {
        lifeSteal = value;
        return this;
    }

    public CharacterStat SetDodge(float value)
    {
        dodge = value;
        return this;
    }

}
public class Character : MonoBehaviour
{
    public bool isDead;
    public bool isDisable;
    public bool isKnockBack;
    public Transform body;
    public CharacterStat stat;
    public SpineController spineController;
    protected CharacterState state = CharacterState.None;

    [SerializeField] protected List<CharacterComponent> componentList;

    Tween knockBackTween;

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        spineController = GetComponentInChildren<SpineController>();
        if (componentList.Count == 0)
        {
            componentList.Clear();
            componentList.AddRange(GetComponentsInChildren<CharacterComponent>());
        }
    }
#endif
    public virtual void OnInit()
    {
        isDisable = isDead = false;
        spineController.OnInits(this);
        foreach (var comp in componentList)
            comp.OnInits(this);
    }

    public T GetCom<T>() where T : CharacterComponent
    {
        foreach (var comp in componentList)
            if (comp is T)
                return comp as T;
        return null;
    }
    public void SetState(CharacterState playerState)
    {
        if (state == playerState || isDead)
            return;
        state = playerState;
        spineController.SetAnim(state);
    }

    public virtual void OnDead()
    {
        SetState(CharacterState.Die);
        isDead = true;
    }

    public virtual void SetStat()
    {

    }

    public virtual void LifeSteal(float hp)
    {
    }

    public virtual float TakeDamage(float damage, bool isCrit)
    {
        return damage;
    }

    public void DisableEnemy(bool isDisable)
    {
        if (isDead)
            return;
        this.isDisable = isDisable;
    }

    public virtual void OnHitDone()
    {
        
    }

    public virtual void KnockBack(Vector2 dir, float force)
    {
        if (isKnockBack || force <=0)
            return;
        isKnockBack = true;

        knockBackTween = GameManager.Instance.DelayedCall(GameManager.Instance.gameConfig.forceBackTime, StopKnockBack);
    }

    public virtual void StopKnockBack()
    {
        isKnockBack = false;
        knockBackTween.Kill();
    }

    public void SetArmor(float value)
    {
        stat.armor = value;
    }

    public virtual void OnLose()
    {
        foreach (var comp in componentList)
            comp.OnLose();
    }

   
}
