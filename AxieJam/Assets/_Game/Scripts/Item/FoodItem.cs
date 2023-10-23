using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    FoodConfig config;

    public void SetConfig(FoodConfig config)
    {
        this.config = config;
        spriteRenderer.sprite = config.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();
        if (player)
        {
            DataManager.Instance.GetData<DataUser>().UpdateFoodItem(config.type, 1);
            PoolManager.Instance.DespawnObject(transform);
        }
    }
}
