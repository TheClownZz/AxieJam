using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    
    public bool isActive;
    [SerializeField] PlayerAsset asset;


    public T GetPCom<T>() where T : PlayerComponent
    {
        foreach (var comp in componentList)
            if (comp is T)
                return comp as T;
        return null;
    }
    public override void OnInit()
    {
        SetStat();
        base.OnInit();
    }


    public void StartLevel()
    {
        isDead = false;
        isActive = true;
        foreach (var comp in componentList)
            comp.OnStartLevel();
        SetState(CharacterState.Alive);
        transform.position = Vector3.zero;
    }

    public void OnUpdate(float dt)
    {
        if (!isActive || isDead)
            return;
        foreach (var comp in componentList)
            comp.OnUpdate(dt);
    }

    public override void OnDead()
    {
        base.OnDead();
        foreach (var comp in componentList)
            comp.OnDead();
        GameManager.Instance.OnLoss();
        SetState(CharacterState.Die);
    }

    public override void SetStat()
    {
        var data = asset.data;

        stat.hp = data.hp;
        stat.armor = data.armor;
        stat.damage = data.damage;
        stat.critRate = data.critRate;
        stat.critDamage = data.critDamage;
        stat.attackSpeed = data.attackSpeed;
        stat.moveSpeed = data.moveSpeed;
    }

    public override void LifeSteal(float hp)
    {
        GetPCom<PlayerHp>().Regen(hp);
    }

    public override float TakeDamage(float damage, bool isCrit)
    {
        return GetPCom<PlayerHp>().TakeDamage(damage, isCrit);
    }

}
