using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerComponent
{
    [SerializeField] Rigidbody2D r2d;
    [SerializeField] float speed;
    public bool allowMove;

    Player pControl;
    Joystick joystick;
    Vector2 direction;
    float currentSpeed;


    public override void OnInits(Character p)
    {
        base.OnInits(p);
        pControl = (Player)p;
        currentSpeed = speed;
    }

    public override void OnWin()
    {
        base.OnWin();
        r2d.velocity = Vector2.zero;
        control.SetState(CharacterState.Idle);
    }
    public override void OnDead()
    {
        base.OnDead();
        r2d.velocity = Vector2.zero;
    }
    public override void OnStartLevel()
    {
        base.OnStartLevel();
        allowMove = true;
    }
    public void SetJoyStick(Joystick joystick)
    {
        this.joystick = joystick;
    }

    public override void OnUpdate(float dt)
    {
        if (!allowMove || !joystick)
            return;
        Facing();
        Move();
    }

    void Move()
    {
        direction = joystick.Direction;
        r2d.velocity = currentSpeed * direction;
    }

    private void Facing()
    {
        Debug.LogError("Facing:"+ joystick.Direction.x);

        if (direction.x * joystick.Direction.x <= 0 && joystick.Direction.x != 0)
        {
            float face = joystick.Direction.x > 0 ? -1 : 1;
            pControl.spineController.FlipX(face);
            Debug.LogError(face);
        }
    }

}
