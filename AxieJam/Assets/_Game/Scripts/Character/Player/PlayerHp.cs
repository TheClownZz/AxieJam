using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHp : PlayerComponent, ITakeDamage
{
    [SerializeField] float hitTime = 0.2f;
    [SerializeField] float currentHp;
    [SerializeField] ItemAvt itemAvt;

    float maxHp;
    float regen;
    bool allowTakeDamge;

    Player pControl;
    WaitForSeconds delay;
    CameraShake camShake;
    Coroutine hitCoroutine;
    Coroutine regenCoroutine;
    private void Awake()
    {
        delay = new WaitForSeconds(1);
        camShake = Camera.main.GetComponent<CameraShake>();
    }
    public override void OnInits(Character player)
    {
        base.OnInits(player);
        pControl = (Player)player;
        currentHp = maxHp = control.stat.hp;
        regen = control.stat.regen;
    }

    public override void OnLose()
    {
        base.OnLose();
        if (regenCoroutine != null)
        {
            StopCoroutine(regenCoroutine);
            regenCoroutine = null;
        }
    }
    public override void OnSelect()
    {
        base.OnSelect();
        allowTakeDamge = true;
        if (regen > 0)
            regenCoroutine = StartCoroutine(IRegen());
    }

    public override void OnUnSelect()
    {
        base.OnUnSelect();
        if (regenCoroutine == null)
            StopCoroutine(regenCoroutine);
        if (hitCoroutine != null)
            StopCoroutine(hitCoroutine);
    }

    public float TakeDamage(float damage, bool isCrit)
    {
        if (!allowTakeDamge || damage == 0 || pControl.isDead || GameManager.Instance.gameState == GameState.Ready)
            return 0;
        allowTakeDamge = false;

        float damageRate = damage / (damage + control.stat.armor);
        damage = damage * damageRate;

        camShake.BigShake();
        Sethp(currentHp - damage);
        hitCoroutine = StartCoroutine(Ihit());
        AudioManager.Instance.PlayOnceShot(AudioType.Hit);

        if (currentHp <= 0)
        {
            pControl.OnDead();
        }
        return damage;
    }

    IEnumerator IRegen()
    {
        while (true)
        {
            yield return delay;
            if (pControl.isActive && !pControl.isDead)
            {
                Regen(regen);
            }
        }
    }

    IEnumerator Ihit()
    {
        float time = 0;
        float maxTime = 0.5f;
        float flashTime = 0.1f;

        bool isShow = true;
        while (time < maxTime)
        {
            isShow = !isShow;
            control.spineController.ShowRender(isShow);
            yield return new WaitForSeconds(flashTime);
            time += flashTime;
        }

        allowTakeDamge = true;
        control.spineController.ShowRender(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (control.isDead)
            return;
        ICreateDamage hitter = collision.GetComponent<ICreateDamage>();
        if (hitter != null)
        {
            hitter.CreateDamage(control);
        }
    }

    private void Sethp(float hp)
    {
        currentHp = hp;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        float percen = currentHp / maxHp;
        itemAvt.UpdateHealth(percen);
    }

    public void Regen(float hp)
    {
        if (currentHp < maxHp)
            Sethp(Mathf.Min(currentHp + hp, maxHp));
    }
    public void SetItem(ItemAvt item)
    {
        itemAvt = item;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        float percen = currentHp / maxHp;
        itemAvt.UpdateHealth(percen, 0);
    }
}

