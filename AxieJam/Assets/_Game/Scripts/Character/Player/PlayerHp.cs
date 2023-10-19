using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[System.Serializable]
public class HpInfo
{
    public float percen;
    public Color color = Color.green;
}
public class PlayerHp : PlayerComponent, ITakeDamage
{
    const float animTime = 0.05f;

    [SerializeField] float currentHp;
    [SerializeField] Image imgHp;
    [SerializeField] List<HpInfo> hpInfoList;
    [SerializeField] Color damageColor = Color.magenta;
    float maxHp;
    float regen;

    WaitForSeconds delay;

    Player pControl;

    float timeHaptic = 0;

    Coroutine coroutine;
    private void Awake()
    {
        delay = new WaitForSeconds(1);
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
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
    public override void OnSelect()
    {
        base.OnSelect();
        UIManager.Instance.GetScreen<ScreenGame>().SetHp(currentHp);
        coroutine = StartCoroutine(IRegen());
    }

    public override void OnUnSelect()
    {
        base.OnUnSelect();
        if (coroutine == null)
            StopCoroutine(coroutine);
    }

    public float TakeDamage(float damage, bool isCrit)
    {
        if (damage == 0 || pControl.isDead || GameManager.Instance.gameState == GameState.Ready)
            return 0;
        float damageRate = damage / (damage + control.stat.armor);
        damage = damage * damageRate;

        Sethp(currentHp - damage);
        imgHp.color= damageColor;

        if (currentHp <= 0 && !GameManager.Instance.isCheat)
        {
            pControl.OnDead();
        }

        if (Time.time - timeHaptic > 0.1f)
        {
            timeHaptic = Time.time;
            AudioManager.Instance.PlayOnceShot(AudioType.Hit);
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

    public void Regen(float hp)
    {
        Sethp(Mathf.Min(currentHp + hp, maxHp));
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
        imgHp.DOKill();
        imgHp.DOFillAmount(percen, animTime).OnComplete(UpdateColorHp);
        UIManager.Instance.GetScreen<ScreenGame>().SetHp(currentHp);
    }

    public void ResetHp()
    {
        Sethp(maxHp);
    }

    private void UpdateColorHp()
    {
        float percen = currentHp / maxHp;
        foreach (var info in hpInfoList)
        {
            if (percen <= info.percen)
            {
                imgHp.color = info.color;
                break;
            }
        }
    }
}

