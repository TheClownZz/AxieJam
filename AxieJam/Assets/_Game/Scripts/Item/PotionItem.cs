using UnityEngine;

public class PotionItem : MonoBehaviour
{
    [SerializeField] GameObjectGetter prefabGetter;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AudioGetter audioGetter;
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
            Transform clone = PoolManager.Instance.SpawnObject(prefabGetter.prefab.transform);
            clone.position = transform.position;
            GameManager.Instance.DelayedCall(0.5f, () =>
            {
                PoolManager.Instance.DespawnObject(clone);
            });
            AudioManager.Instance.PlaySound(audioGetter.clip);

        }
    }
}
