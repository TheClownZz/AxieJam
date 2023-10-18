using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public bool isActive;
    [SerializeField] PlayerType type;
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
        stat.SetHp(data.hp)
            .Setarmor(data.armor)
            .SetDamage(data.damage)
            .SetCritRate(data.critRate)
            .SetMoveSpeed(data.moveSpeed)
            .SetCritDamage(data.critDamage)
            .SetAttackSpeed(data.attackSpeed);


        int level = DataManager.Instance.GetData<DataUser>().GetLevel(type);
        for (int i = 1; i < level; i++)
        {
            PlayerLevelConfig config = data.GetLevelConfig(i - 1);
            stat.SetHp(config.hp + stat.hp)
                .Setarmor(config.armor + stat.armor)
                .SetDamage(config.damage + stat.damage)
                .SetCritRate(config.critRate + stat.critRate)
                .SetMoveSpeed(config.moveSpeed + stat.moveSpeed)
                .SetCritDamage(config.critDamage + stat.critDamage)
                .SetAttackSpeed(config.attackSpeed + stat.attackSpeed);

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

}
