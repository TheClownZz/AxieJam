using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerComponent
{
    [SerializeField] Weapon weapon;
    [SerializeField] Setcursor setcursor;

    Camera mainCamera;
    public override void OnInits(Character control)
    {
        base.OnInits(control);
        weapon.OnInits(control);
        mainCamera = Camera.main;
    }
    public override void OnUpdate(float dt)
    {
        Facing();
        if (Input.GetMouseButton(0))
        {
            weapon.OnUpdate(dt);
        }

    }


    private void Facing()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        Transform wpTrans = weapon.transform;
        Vector2 direction = new Vector2(
            mousePosition.x - wpTrans.position.x,
            mousePosition.y - wpTrans.position.y
        );

        wpTrans.right = -direction;
    }
}

