using UnityEngine;
public class BossSumon : BossAttack
{
    int maxEnemy = 5;
    int numberEnemy;
    [SerializeField] EnemyType enemyType;

    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        BossSkillConfig config = control.GetComponent<SetupBossData>().asset.data.skilldDataList[skillIndex];
        coolDown = config.GetSkillValue(SkillType.Cooldown, 3);
        numberEnemy = (int)config.GetSkillValue(SkillType.Number, 1);
        timeAttack = Time.time - coolDown / 2;
    }
    public override void OnUpdate(float dt)
    {
        if (!control) return;
        base.OnUpdate(dt);

        if (Time.time - timeAttack >= coolDown)
        {
            OnAttack();
        }
    }

    public override void OnAttack()
    {
        if (control.GetCom<BossMove>().isJump) return;
        timeAttack = Time.time;
        if (GameManager.Instance.levelController.enemyList.Count < maxEnemy)
        {
            control.DisableEnemy(true);
            control.SetState(CharacterState.Attack);
            AudioManager.Instance.PlaySound(attackClip);
        }

    }

    public override void Attacktarget()
    {
        for (int i = 0; i < numberEnemy; i++)
        {
            GameManager.Instance.levelController.SpawnEnemy(enemyType, control.GetComponent<Enemy>().waveStat);
        }
    }
}
