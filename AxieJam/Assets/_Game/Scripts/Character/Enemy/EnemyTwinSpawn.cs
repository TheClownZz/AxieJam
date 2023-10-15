using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwinSpawn : EnemyComponent
{
    [SerializeField] Enemy pf;
    [SerializeField] int numberSpawn = 2;
    [SerializeField] float statRate = 0.5f;
    public override void OnDead()
    {
        base.OnDead();
        SpawnTwin();
    }

    private void SpawnTwin()
    {
        for (int i = 0; i < numberSpawn; i++)
        {
            Vector3 pos = control.transform.position + (Vector3)Random.insideUnitCircle;
            Enemy e = GameManager.Instance.levelController.SpawnEnemy(pf.transform);
            e.stat.SetStat(control.stat);
            e.stat.hp *= statRate;
            e.stat.armor *= statRate;
            e.stat.damage *= statRate;
            e.transform.position = pos;
            e.OnInit();
        }
    }
}
