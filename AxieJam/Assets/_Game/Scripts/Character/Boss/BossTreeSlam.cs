using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTreeSlam : BossBigSlam
{
    float radius = 3f;
    public override Vector3 GetSpawnPos()
    {
        Vector3 spawnPos = GameManager.Instance.GetCurrentPlayer().transform.position + (Vector3)(radius * Random.insideUnitCircle);
        return GameManager.Instance.levelController.GetSpawnErea(spawnPos);

    }
}
