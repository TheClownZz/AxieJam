
using UnityEngine;

public class EnemyBullet : Bullet
{
    [SerializeField] SpriteRenderer bulletRender;

    public void SetSprite(Sprite sprite)
    {
        bulletRender.sprite = sprite;
    }
    protected override void Update()
    {
        base.Update();
        if (bulletRender && !bulletRender.isVisible)
        {
            Clear();
        }
    }
}
