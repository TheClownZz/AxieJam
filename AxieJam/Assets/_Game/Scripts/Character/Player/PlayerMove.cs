using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerComponent
{
    public bool allowMove;
    [SerializeField] Rigidbody2D body;
    [SerializeField] float currentSpeed;
    [SerializeField] float faceDistance = 0.25f;
    [SerializeField] float moveDistance = 0.5f;

    Camera cam;
    Player pControl;
    Vector3 mousePos;
    Vector2 direction;

    public override void OnInits(Character p)
    {
        base.OnInits(p);
        cam = Camera.main;
        pControl = (Player)p;
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
    public override void OnStartLevel()
    {
        base.OnStartLevel();
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
            if (dif.sqrMagnitude > faceDistance)
            {
                Facing(dif.normalized);
                direction = dif.normalized;
            }
            if (dif.sqrMagnitude > moveDistance)
            {
                body.velocity = currentSpeed * dif.normalized;
            }
            else
            {
                body.velocity = Vector2.zero;
            }
            control.SetState(CharacterState.Run);
        }

        mousePos = mouseWorldPos;
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
