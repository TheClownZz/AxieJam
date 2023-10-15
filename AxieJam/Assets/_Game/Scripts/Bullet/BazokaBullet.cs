using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BazokaBullet : Bullet
{
    const float offset = 0.25f;

    [SerializeField] float turnSpeed = 10;

    Vector3 cachedPosition;
    Character target;
    float speed;
    float radius;

    LayerMask targetLayer;

    public override void OnInits(Weapon weapon, float speed, Vector3 dir)
    {
        base.OnInits(weapon, speed, dir);
        this.dir = -dir;
        this.speed = speed;
    }

    public void SetTarget(Character target)
    {
        this.target = target;
        targetLayer = (1 << target.gameObject.layer);
    }

    public void SetRadius(float radius)
    {
        this.radius = radius;
    }

    protected override void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
        FollowTarget();
    }

    protected void FollowTarget()
    {
        if (target)
        {
            if (target.isDead)
            {
                target = null;
            }
            else
            {
                cachedPosition = target.transform.position;
            }
        }
        float distance = Vector3.Distance(cachedPosition, transform.position);

        if (distance <= offset)
        {
            Explosion();
        }
        else
        {
            Vector3 newDir = (cachedPosition - transform.position).normalized;
            dir = Vector3.Lerp(dir, newDir, turnSpeed * Time.deltaTime);
            dir.Normalize();

            Quaternion q = Quaternion.FromToRotation(Vector3.up, dir);
            transform.rotation = q;
        }
    }

    private void Explosion()
    {
        var hitList = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        foreach (var hit in hitList)
        {
            Character c = hit.GetComponent<Character>();
            if (IsMaxHit())
                return;

            PreHit(c);
            HitCharacter(c);
            AfterHit(c);

            hitCount += 1;
            if (IsMaxHit())
                Clear();
        }
        Clear();
        SpawnHitFx();
    }
    public override void CreateDamage(Character character)
    {
        if (target != null && !target.isDead && character == target)
        {
            Explosion();
        }
    }

    public override bool IsMaxHit()
    {
        return false;
    }

}
