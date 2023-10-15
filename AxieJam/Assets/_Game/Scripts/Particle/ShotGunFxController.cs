using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunFxController : FxController
{
    public override void SetRange(float range)
    {
        base.SetRange(range);
        transform.localScale = range * Vector3.one;
    }

    public override void Play()
    {
        gameObject.SetActive(true);
    }

    public override void Stop()
    {
        gameObject.SetActive(false);
    }
}
