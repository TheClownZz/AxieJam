using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossSkillInfo
{
    public EnemyAttack controller;
    public string animName;
    public float attackTime;
}
public class Boss : Enemy
{
    [SerializeField] List<BossSkillInfo> bossSkillInfoList;

    int skillIndex;
    public override void OnInit()
    {
        base.OnInit();
        skillIndex = 0;
        ((EnemySpineController)spineController).SetAttack(bossSkillInfoList[skillIndex].animName, bossSkillInfoList[skillIndex].attackTime);
        foreach(var skill in bossSkillInfoList)
        {
            componentList.Remove(skill.controller);
        }
        componentList.Add(bossSkillInfoList[skillIndex].controller);
        bossSkillInfoList[skillIndex].controller.gameObject.SetActive(true);
        bossSkillInfoList[skillIndex].controller.OnInits(this);
    }
    public override float TakeDamage(float damage, bool isCrit)
    {
        EnemyHp eHp = GetECom<EnemyHp>();
        float hpLost = eHp.TakeDamage(damage, isCrit);
        if (skillIndex < bossSkillInfoList.Count - 1 && eHp.currentHp < 0.5f * stat.hp)
        {
            componentList.Remove(bossSkillInfoList[skillIndex].controller);
            bossSkillInfoList[skillIndex].controller.gameObject.SetActive(false);
            skillIndex += 1;
            ((EnemySpineController)spineController).SetAttack(bossSkillInfoList[skillIndex].animName, bossSkillInfoList[skillIndex].attackTime);
            componentList.Add(bossSkillInfoList[skillIndex].controller);
            bossSkillInfoList[skillIndex].controller.OnInits(this);
            bossSkillInfoList[skillIndex].controller.gameObject.SetActive(true);

        }
        return hpLost;
    }

    public override void SetStat()
    {
        var data = GetComponent<SetupBossData>().asset.data;
        stat.SetHp(data.hp)
            .Setarmor(data.armor)
            .SetDamage(data.damage)
            .SetAttackSpeed(data.attackSpeed)
            .SetMoveSpeed(data.moveSpeed);
    }
}
