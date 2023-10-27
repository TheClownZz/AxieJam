using System.Collections;
using UnityEngine;

public class HealingWard : MonoBehaviour
{
    Player target;
    WaitForSeconds delay;
    protected float cooldown;
    protected float activeValue;
    [SerializeField] Transform fxHeal;
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
        OnDeActive();
    }

    protected virtual void OnDeActive()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnActive()
    {
        if (target)
        {
            Transform clone = PoolManager.Instance.SpawnObject(fxHeal);
            clone.position = target.transform.position;
            GameManager.Instance.DelayedCall(0.5f, () =>
            {
                PoolManager.Instance.DespawnObject(clone);
            });
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
