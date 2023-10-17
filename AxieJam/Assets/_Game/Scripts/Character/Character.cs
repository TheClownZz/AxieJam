using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimKey
{
    public const string Die = "Die";
    public const string Run = "Run";
    public const string Idle = "Idle";
    public const string Alive = "Alive";
    public const string Speed = "Speed";
    public const string Attack = "Attack";
}

public enum CharacterState
{
    None,
    Alive,
    Idle,
    Run,
    Die,
    Attack,
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

    public void SetStat(CharacterStat stat)
    {
        hp = stat.hp;
        regen = stat.regen;
        armor = stat.armor;
        damage = stat.damage;
        critRate = stat.critRate;
        critDamage = stat.critDamage;
        attackSpeed = stat.attackSpeed;
        moveSpeed = stat.moveSpeed;
        lifeSteal = stat.lifeSteal;
        dodge = stat.dodge;

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
        spineController.OnInits();
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
        if (isDisable)
            spineController.Pause();
        else
            spineController.Resume();
    }

    public virtual void KnockBack(Vector2 dir, float force)
    {
        if (isKnockBack || force <=0)
            return;
        isKnockBack = true;

        spineController.Pause();
        knockBackTween = DOVirtual.DelayedCall(GameManager.Instance.gameConfig.forceBackTime, StopKnockBack);
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
