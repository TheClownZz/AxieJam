using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class MeleeWeapon : Weapon
{
    [SerializeField] protected Hitbox hitBox;
    [SerializeField] protected Transform hitter;
    [SerializeField] protected FxController particleController;
    [SerializeField] float minFxTime = 0.5f;
    bool isCheckHit;
    float effectTime;


    public override void OnInits(Character characterControl)
    {
        base.OnInits(characterControl);
        hitBox.OnInits(this);
    }
    public override void SetTier(int tier)
    {
        base.SetTier(tier);
        hitter.transform.localScale = stat.range * Vector3.one;
        if (particleController)
        {
            particleController.transform.SetParent(null);
            particleController.SetRange(stat.range);
        }

        effectTime = MathF.Min(minFxTime, coolDown);

    }
    protected override void FaceToTarget(float dt)
    {
        Vector2 dir = attckController.GetTarget().position - characterControl.transform.position;
        Quaternion q = Quaternion.FromToRotation(Vector3.up, dir);
        transform.rotation = q;
    }

    public override void OnAttack()
    {
        base.OnAttack();
        if (particleController)
            PlayeEffect();
        hitBox.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (hitBox.gameObject.activeInHierarchy)
            isCheckHit = true;
    }

    private void LateUpdate()
    {
        if (isCheckHit)
        {
            isCheckHit = false;
            hitBox.SetActive(false);
        }
    }

    private void PlayeEffect()
    {
        particleController.transform.position = hitter.position;
        particleController.transform.rotation = hitter.rotation;
        particleController.Play();
        DOVirtual.DelayedCall(effectTime, () =>
        {
            particleController.Stop();
        });
    }

    public override void OnClear()
    {
        particleController.transform.SetParent(hitter.transform);
        base.OnClear();
    }
}
