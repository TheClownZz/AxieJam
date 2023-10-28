using System.Collections;
using UnityEngine;

public class PlayerAttack : PlayerComponent
{
    const float attackRate = 0.15f;

    float attackTime;
    float cooldown;
    float currentCooldown;
    float updateTime = 0.1f;
    float spawnCooldown = 6;



    ItemAvt item;
    Camera mainCamera;
    WaitForSeconds delay;
    Coroutine updateCoroutine;
    [SerializeField] PlayerGun weapon;
    [SerializeField] SkillConfig config;

    public override void OnInits(Character control)
    {
        base.OnInits(control);
        weapon.OnInits(control);
        mainCamera = Camera.main;
        delay = new WaitForSeconds(updateTime);
        updateCoroutine = StartCoroutine(IUpdateCoolDown());
    }

    public void SetConfig(SkillConfig config)
    {
        this.config = config;
        cooldown = config.defaultValue.cooldown;
        SetCurrentCooldown(config.defaultValue.cooldown);
    }

    public void SetItem(ItemAvt item)
    {
        this.item = item;
    }

    public override void OnSelect()
    {
        base.OnSelect();
        weapon.OnSelect();
        cooldown = Mathf.Max(cooldown, spawnCooldown);
        SetCurrentCooldown(Mathf.Max(currentCooldown, spawnCooldown));
    }

    public override void OnUnSelect()
    {
        base.OnUnSelect();
        weapon.DeAvtiveSkill();
    }

    public override void OnLose()
    {
        base.OnLose();
        StopCoroutine(updateCoroutine);
    }


    public override void OnUpdate(float dt)
    {
        Facing();
        if (Input.GetMouseButtonDown(0) ||
            (Input.GetMouseButton(0) && Time.time - attackTime > attackRate))
        {
            attackTime = Time.time;
            weapon.OnAttack();
        }

        if (Input.GetKeyDown(KeyCode.F) && currentCooldown <= 0)
        {
            weapon.ActiveSKill(config);
            cooldown = Mathf.Max(cooldown, spawnCooldown);
            SetCurrentCooldown(config.defaultValue.cooldown);
        }
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            weapon.ActiveSKill(config);
            cooldown = Mathf.Max(cooldown, spawnCooldown);
            SetCurrentCooldown(config.defaultValue.cooldown);
        }
#endif
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
                SetCurrentCooldown(currentCooldown - updateTime);
                item.UpdateMana(1 - currentCooldown / cooldown);
            }
        }
    }

    private void SetCurrentCooldown(float value)
    {
        currentCooldown = value;
    }
}

