using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItem : MonoBehaviour
{
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
            PoolManager.Instance.DespawnObject(transform);
        }
    }
}
