using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public bool isActive;
    PlayerConfig config;
    public PlayerType type;


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


    public void OnSelect()
    {
        isDead = false;
        isActive = true;
        foreach (var comp in componentList)
            comp.OnSelect();
        spineController.ShowRender(true);
        SetState(CharacterState.Alive);
    }

    public void OnUnSelect()
    {
        foreach (var comp in componentList)
            comp.OnUnSelect();
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
        stat.SetHp(config.hp)
            .Setarmor(config.armor)
            .SetDamage(config.damage)
            .SetCritRate(config.critRate)
            .SetMoveSpeed(config.moveSpeed)
            .SetCritDamage(config.critDamage)
            .SetAttackSpeed(config.attackSpeed);


        int level = DataManager.Instance.GetData<DataUser>().GetLevel(type);
        for (int i = 1; i < level; i++)
        {
            PlayerLevelConfig lvConfig = config.GetLevelConfig(i - 1);
            stat.SetHp(lvConfig.hp + stat.hp)
                .Setarmor(lvConfig.armor + stat.armor)
                .SetDamage(lvConfig.damage + stat.damage)
                .SetCritRate(lvConfig.critRate + stat.critRate)
                .SetMoveSpeed(lvConfig.moveSpeed + stat.moveSpeed)
                .SetCritDamage(lvConfig.critDamage + stat.critDamage)
                .SetAttackSpeed(lvConfig.attackSpeed + stat.attackSpeed);

        }

    }

    public override void LifeSteal(float hp)
    {
        GetPCom<PlayerHp>().Regen(hp);
    }

    public override float TakeDamage(float damage, bool isCrit)
    {
        return GetPCom<PlayerHp>().TakeDamage(damage, isCrit);
    }

    public void SetData(PlayerConfig data)
    {
        config = data;
        GetCom<PlayerAttack>().SetConfig(
            config.GetSkillConfig(DataManager.Instance.GetData<DataUser>().GetLevel(type)));
    }

    public void SetItemAvt(ItemAvt itemAvt)
    {
        GetCom<PlayerHp>().SetItem(itemAvt);
        GetCom<PlayerAttack>().SetItem(itemAvt);
    }
}
