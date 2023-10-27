using UnityEngine;
using PathologicalGames;
using DG.Tweening;

public class PoolElement : MonoBehaviour
{
    [HideInInspector] protected SpawnPool pool = null;
    public void AssignSelf(SpawnPool pool)
    {
        this.pool = pool;
        // OnSpawned();
    }

    public void DespawnSelf()
    {
        try
        {
            if (pool != null)
            {
                PoolManager.Instance.DespawnObject(this.transform);
                // pool.Despawn(this.transform);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        catch (System.Exception ex)
        {
            ex.Log();
        }
        // OnDespawned();
    }

    public void DespawnSelf(float delay)
    {
        if (delay > 0f)
        {
            DOVirtual.DelayedCall(delay, delegate
            {
                DespawnSelf();
            });
        }
        else
        {
            DespawnSelf();
        }
    }

    public virtual void OnSpawned()
    {
    }

    public virtual void OnDespawned() { }

    public virtual void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public virtual void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public virtual void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public virtual void SetEulerAngles(Vector3 euler)
    {
        transform.eulerAngles = euler;
    }
}
