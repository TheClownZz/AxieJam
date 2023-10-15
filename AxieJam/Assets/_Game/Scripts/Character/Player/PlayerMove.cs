using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerComponent
{
    public bool allowMove;
    [SerializeField] float currentSpeed;
    [SerializeField] Rigidbody2D body;

    Joystick joystick;
    Vector2 direction;

    PlayerAttack pAttack;
    Player pControl;

    public override void OnInits(Character p)
    {
        base.OnInits(p);
        pControl = (Player)p;
        currentSpeed = p.stat.moveSpeed;
        pAttack = pControl.GetPCom<PlayerAttack>();
    }

    public override void OnWin()
    {
        base.OnWin();
        body.velocity = Vector2.zero;
        control.SetState(CharacterState.Idle);
    }
    public override void OnDead()
    {
        base.OnDead();
        body.velocity = Vector2.zero;
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
        Move();
    }
    void Move()
    {
        if (!pAttack.target || pAttack.target.isDead)
            Facing();
        direction = joystick.Direction;
        body.velocity = currentSpeed * direction;

        UpdateState();
    }


    private void Facing()
    {
        if (direction.x * joystick.Direction.x <= 0 && joystick.Direction.x != 0)
        {
            float face = joystick.Direction.x > 0 ? 1 : -1;
            control.anim.FlipX(face);
        }
    }
    private void UpdateState()
    {
        if (direction == Vector2.zero)
        {
            control.SetState(CharacterState.Idle);
        }
        else
        {
            float speed = body.velocity.magnitude * GameManager.Instance.gameConfig.speedFactor;
            if (speed < 0.05)
            {
                control.SetState(CharacterState.Idle);

            }
            else
            {
                control.SetState(CharacterState.Run);
                control.anim.SetSpeed(body.velocity.magnitude * GameManager.Instance.gameConfig.speedFactor);
            }
        }
    }
}
