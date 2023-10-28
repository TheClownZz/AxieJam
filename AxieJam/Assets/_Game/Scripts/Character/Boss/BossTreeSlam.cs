
using UnityEngine;

public class BossTreeSlam : BossBigSlam
{
    public override Vector3 GetSpawnPos()
    {
        Vector3 spawnPos = FrameWorkUtility.SpawnInCircle(GameManager.Instance.GetCurrentPlayer().transform.position, radius, Random.Range(0, 359));
        return GameManager.Instance.levelController.GetSpawnErea(spawnPos);

    }
}
