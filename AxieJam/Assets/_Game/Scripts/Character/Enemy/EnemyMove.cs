using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyMove : EnemyComponent
{
    const float minDistance = 0.15f;
    const float runDistance = 0.35f;
    [SerializeField] protected float baseSpeed;
    [SerializeField] protected float currentSpeed;
    protected Vector3 direction;

    protected Vector3 forceDir;
    protected float forceDrag;
    protected float forceStr;

    protected Enemy eControl;

    Transform top, bot, left, right;

    public override void OnInits(Character e)
    {
        base.OnInits(e);
        forceStr = 0;
        eControl = (Enemy)e;
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

    public override void OnUpdate(float dt)
    {
        if (GetTarget().isDead || control.isDisable) return;
        if (!eControl.isKnockBack)
            Move(dt);
        else
            Force(dt);
        CheckLimit();
    }

    protected virtual void Move(float dt)
    {
        float distance = Vector2.Distance(transform.position, GetTarget().transform.position);

        Vector3 dir = Vector3.zero;
        if (direction == Vector3.zero)
        {
            if (distance > runDistance)
                dir = (GetTarget().transform.position - transform.position).normalized;
        }
        else
        {
            if (distance >= minDistance)
                dir = (GetTarget().transform.position - transform.position).normalized;
        }

        Facing(dir);

        direction = dir;
        UpdatePostion(dt);
        UpdateState();
    }

    protected virtual void UpdatePostion(float dt)
    {
        transform.position += direction * currentSpeed * dt;
    }
    protected virtual void Force(float dt)
    {
        transform.position += forceStr * forceDir * dt;
        forceStr -= forceDrag * dt;
    }

    public Player GetTarget()
    {
        return GameManager.Instance.currentPlayer;
    }


    public void SetForceDir(Vector3 forceDir, float forceStr)
    {
        this.forceDir = forceDir;
        this.forceStr = forceStr;

        forceDrag = forceStr * GameManager.Instance.gameConfig.forceDrag;
    }

    protected void Facing(Vector3 dir)
    {
        if (direction.x * dir.x <= 0 && dir.x != 0)
        {
            float face = dir.x > 0 ? -1 : 1;
            control.spineController.FlipX(face);
        }
    }

    protected void UpdateState()
    {
        if (direction == Vector3.zero)
        {
            control.SetState(CharacterState.Idle);
        }
        else
        {
            control.SetState(CharacterState.Run);
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
