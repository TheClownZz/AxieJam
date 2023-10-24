using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
    [SerializeField] Transform fx;
    [SerializeField] SpriteRenderer spriteRenderer;
    PotionConfig config;

    public void SetConfig(PotionConfig config)
    {
        this.config = config;
        spriteRenderer.sprite = config.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player)
        {
            DataManager.Instance.GetData<DataUser>().UpdatePotionItem(config.type, 1);
            AudioManager.Instance.PlayOnceShot(AudioType.Item);
            PoolManager.Instance.DespawnObject(transform);
            Transform clone = PoolManager.Instance.SpawnObject(fx);
            clone.position = transform.position;
            DOVirtual.DelayedCall(0.5f, () =>
            {
                PoolManager.Instance.DespawnObject(clone);
            });
        }
    }
}
