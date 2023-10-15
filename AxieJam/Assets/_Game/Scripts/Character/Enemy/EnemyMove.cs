using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyMove : EnemyComponent
{
    const float minDistance = 0.15f;
    const float runDistance = 0.35f;
    [SerializeField] float baseSpeed;
    [SerializeField] float currentSpeed;
    protected Vector3 direction;

    protected Vector3 forceDir;
    protected float forceDrag;
    protected float forceStr;

    protected Enemy eControl;
    protected Player target;

    Transform top, bot, left, right;

    public override void OnInits(Character e)
    {
        base.OnInits(e);
        forceStr = 0;
        eControl = (Enemy)e;
        target = GameManager.Instance.player;
        direction = Vector3.zero;

        baseSpeed = Random.Range(e.stat.moveSpeed * 0.9f, e.stat.moveSpeed * 1.1f);
        UpdateSpeed(1);

        LevelController lc = GameManager.Instance.levelController;
        SetLimit(lc.top, lc.bot, lc.left, lc.right);
    }

    public void SetLimit(Transform t, Transform b, Transform l, Transform r)
    {
        top = t;
        bot = b;
        left = l;
        right = r;
    }

    public override void OnLose()
    {
        base.OnLose();
        control.anim.SetSpeed(0);
    }

    public override void OnUpdate(float dt)
    {
        if (target.isDead || eControl.isDisable) return;
        if (!eControl.isKnockBack)
            Move(dt);
        else
            Force(dt);
        CheckLimit();
    }

    private void Move(float dt)
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);

        Vector3 dir = Vector3.zero;
        if(direction == Vector3.zero)
        {
            if(distance > runDistance)
                dir = (target.transform.position - transform.position).normalized;
        }else
        {
            if (distance >= minDistance)
                dir = (target.transform.position - transform.position).normalized;
        }

        Facing(dir);

        direction = dir;
        transform.position += direction * currentSpeed * dt;

        UpdateState();
    }

    protected virtual void Force(float dt)
    {
        transform.position += forceStr * forceDir * dt;
        forceStr -= forceDrag * dt;
    }


    public void SetForceDir(Vector3 forceDir, float forceStr)
    {
        this.forceDir = forceDir;
        this.forceStr = forceStr;

        forceDrag = forceStr * GameManager.Instance.gameConfig.forceDrag;
    }

    private void Facing(Vector3 dir)
    {
        if (direction.x * dir.x <= 0 && dir.x != 0)
        {
            float face = dir.x > 0 ? -1 : 1;
            control.anim.FlipX(face);
        }
    }

    private void UpdateState()
    {
        if (direction == Vector3.zero)
        {
            control.SetState(CharacterState.Idle);
        }
        else
        {
            control.SetState(CharacterState.Run);
            control.anim.SetSpeed(currentSpeed * GameManager.Instance.gameConfig.speedFactor);
        }
    }

    public void UpdateSpeed(float rate)
    {
        currentSpeed = baseSpeed * rate;
    }

    private void CheckLimit()
    {
        Vector3 pos = transform.position;
        if (pos.x < left.position.x || pos.x > right.position.x || pos.y < bot.position.y || pos.y > top.position.y)
        {
            pos.x = Mathf.Clamp(pos.x, left.position.x, right.position.x);
            pos.y = Mathf.Clamp(pos.y, bot.position.y, top.position.y);

            eControl.StopKnockBack();
        }

    }


}
