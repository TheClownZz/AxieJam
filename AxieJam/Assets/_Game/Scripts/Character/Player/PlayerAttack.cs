using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : PlayerComponent
{
    float updateTime = 0.1f;
    float spawnCooldown = 10;
    [SerializeField] PlayerGun weapon;
    [SerializeField] Setcursor setcursor;

    ItemAvt item;
    Camera mainCamera;
    WaitForSeconds delay;
    float currentCooldown;
    PlayerSkillConfig config;

    Coroutine updateCoroutine;
    public override void OnInits(Character control)
    {
        base.OnInits(control);
        weapon.OnInits(control);
        mainCamera = Camera.main;
        delay = new WaitForSeconds(updateTime);
        updateCoroutine = StartCoroutine(IUpdateCoolDown());
    }

    public void SetConfig(PlayerSkillConfig config)
    {
        this.config = config;
        SetCooldown(config.defaultValue.cooldown);
    }

    public void SetItem(ItemAvt item)
    {
        this.item = item;
    }

    public override void OnSelect()
    {
        base.OnSelect();
        SetCooldown(Mathf.Min(currentCooldown, spawnCooldown));
    }

    public override void OnUnSelect()
    {
        base.OnUnSelect();
        weapon.UnAvtiveSkill();
    }

    public override void OnLose()
    {
        base.OnLose();
        StopCoroutine(updateCoroutine);
        Debug.LogError("stop update");
    }
    

    public override void OnUpdate(float dt)
    {
        Facing();
        if (Input.GetMouseButton(0))
        {
            weapon.OnUpdate(dt);
        }
        if (Input.GetKeyDown(KeyCode.F) && currentCooldown <= 0)
        {
            weapon.ActiveSKill();
            SetCooldown(config.defaultValue.cooldown);
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
    IEnumerator IUpdateCoolDown()
    {
        while (true)
        {
            yield return delay;
            if (currentCooldown > 0)
            {
                SetCooldown(currentCooldown - updateTime);
                item.UpdateMana(1 - currentCooldown / config.defaultValue.cooldown);
            }
        }
    }

    private void SetCooldown(float value)
    {
        currentCooldown = value;
    }
}

