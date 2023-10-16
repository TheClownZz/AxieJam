using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : Gun
{
    protected override void FaceToTarget(float dt)
    {
       // Vector2 dir = attckController.GetTarget().position - characterControl.transform.position;
       // Quaternion q = Quaternion.FromToRotation(Vector3.up, dir);
       // transform.rotation = q;
    }
}
