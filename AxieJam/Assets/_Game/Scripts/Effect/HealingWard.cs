using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HealingWard : MonoBehaviour
{
    Player target;
    WaitForSeconds delay;
    protected float cooldown;
    protected float activeValue;
    public void OnInits(float activeValue, float cooldown, float duarion)
    {
        gameObject.SetActive(true);
        this.cooldown = cooldown;
        this.activeValue = activeValue;
        delay = new WaitForSeconds(cooldown);
        StartCoroutine(IActive(duarion));
    }

    IEnumerator IActive(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            yield return delay;
            OnActive();
            time += cooldown;
        }
        gameObject.SetActive(false);
    }

    protected virtual void OnActive()
    {
        if (target)
        {
            target.GetCom<PlayerHp>().RegenPercen(activeValue);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!target)
            target = collision.GetComponent<Player>();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (target && target == collision.GetComponent<Player>())
        {
            target = null;
        }
    }
}
