using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerComponent
{
    public bool allowMove;
    [SerializeField] Rigidbody2D body;
    [SerializeField] float currentSpeed;
    [SerializeField] float faceDistance = 0.25f;
    [SerializeField] float moveDistance = 1f;
    [SerializeField] float minMoveDistance = 0.2f;

    Camera cam;
    Vector2 direction;

    public override void OnInits(Character p)
    {
        base.OnInits(p);
        cam = Camera.main;
        currentSpeed = p.stat.moveSpeed;
    }

    public override void OnCompleteLevel()
    {
        base.OnCompleteLevel();
        body.velocity = Vector2.zero;
        control.SetState(CharacterState.Idle);
    }
    public override void OnDead()
    {
        base.OnDead();
        allowMove = false;
        body.velocity = Vector2.zero;
    }
    public override void OnSelect()
    {
        base.OnSelect();
        allowMove = true;
    }
    public override void OnUpdate(float dt)
    {
        if (!allowMove) return;
        Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = body.transform.position.z;
        Vector2 dif = mouseWorldPos - transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            Facing(dif.normalized);
            direction = Vector2.zero;
            body.velocity = Vector2.zero;
            control.SetState(CharacterState.Idle);
        }
        else if (Input.GetMouseButton(0))
        {
            Facing(dif.normalized);
        }
        else
        {
            float distance = dif.sqrMagnitude;
            if (distance > faceDistance)
            {
                Facing(dif.normalized);
                direction = dif.normalized;
            }
            if (distance > moveDistance || body.velocity != Vector2.zero && distance > minMoveDistance)
            {
                body.velocity = currentSpeed * dif.normalized;
                control.SetState(CharacterState.Run);

            }
            else
            {
                body.velocity = Vector2.zero;
                control.SetState(CharacterState.Idle);
            }
        }
    }

    private void Facing(Vector2 dir)
    {
        if (direction.x * dir.x <= 0 && dir.x != 0)
        {
            float face = dir.x > 0 ? -1 : 1;
            control.spineController.FlipX(face);
        }
    }

    public void ForceBack(Vector2 force)
    {
        body.velocity = Vector2.zero;
        body.AddForce(force);
    }
}
