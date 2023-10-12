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
    public Transform body;
    public CharacterStat stat;
    public SpineController spineController;
    protected CharacterState state = CharacterState.None;

    [SerializeField] protected List<CharacterComponent> componentList;

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
  //      spineController.SetAnim(state);
    }

    public virtual void OnDead()
    {
        SetState(CharacterState.Die);
        isDead = true;
    }

    public virtual void LifeSteal(float hp)
    {
    }

    public virtual float TakeDamage(float damage, bool isCrit)
    {
        return damage;
    }


    public virtual void OnWin()
    {
        foreach (var comp in componentList)
            comp.OnWin();
    }

    public virtual void OnLose()
    {
        foreach (var comp in componentList)
            comp.OnLose();
    }
}
