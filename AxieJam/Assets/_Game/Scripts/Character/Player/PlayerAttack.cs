using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerComponent, IAttack
{
    [SerializeField] List<Weapon> currentWpList;
    protected List<Enemy> enemyList;
    public Enemy target;

    float maxDistance;

    public override void OnInits(Character player)
    {
        base.OnInits(player);
        enemyList = GameManager.Instance.levelController.GetEnemyList();
    }

    public override void OnStartLevel()
    {
        base.OnStartLevel();
        SetupWp();
    }

    private void SetupWp()
    {
        foreach (var weapon in currentWpList)
        {
            weapon.OnClear();
        }
        currentWpList.Clear();

        List<DataWeaponSave> wpDatalist = DataManager.Instance.GetData<DataUser>().DataSave.currentWeaponList;
        WeaponListAsset asset = DataManager.Instance.GetAsset<WeaponListAsset>();

        foreach (var data in wpDatalist)
        {
            if (data.weaponType == WeaponType.None)
                continue;
            Weapon wp = asset.GetAsset(data.weaponType).pf;
            var currentWp = PoolManager.Instance.SpawnObject(wp.transform).GetComponent<Weapon>();
            currentWp.SetController(this);
            currentWp.OnInits(control);
            currentWp.SetTier(data.tier);
            currentWp.SetDamageRate(1);
            currentWpList.Add(currentWp);

        }

        maxDistance = 0;
        foreach (var weapon in currentWpList)
        {
            if (maxDistance < weapon.stat.range)
            {
                maxDistance = weapon.stat.range;
            }
        }
    }

    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
        target = GetNearEnemy();
        if (target && !target.isDead)
        {
            Facing();
            foreach (var wp in currentWpList)
                wp.OnUpdate(dt);
        }
    }

    public Enemy GetNearEnemy()
    {
        Enemy result = null;
        float curDistance = maxDistance;
        foreach (Enemy enemy in enemyList)
        {
            if (enemy.isDead || !enemy.gameObject.activeInHierarchy)
                continue;
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < curDistance)
            {
                curDistance = distance;
                result = enemy;
            }
        }
        return result;
    }

    public Character GetControl()
    {
        return control;
    }

    private void Facing()
    {
        float face = target.transform.position.x > control.body.position.x ? 1 : -1;
        control.anim.FlipX(face);
    }

    public Transform GetTarget()
    {
        return target ? target.transform : null;
    }

    public void OnWeaponUpdate()
    {
    }
}
