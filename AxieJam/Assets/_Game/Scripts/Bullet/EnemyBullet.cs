using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    
    protected override void Update()
    {
        base.Update();
        if (bulletRender && !bulletRender.isVisible)
        {
            Clear();
        }
    }
}
