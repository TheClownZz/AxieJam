using DG.Tweening;
using UnityEngine;

public class OneHitBullet : Bullet
{
    float oneHitRate = 0.1f;

    public void SetOneHitRate(float value)
    {
        oneHitRate = value;
    }

    protected override void HitCharacter(Character character)
    {
        bool isOneHit = Random.value <= oneHitRate;
        if (isOneHit)
        {
            character.OnDead();
            SpawnText(character);
          //  SpawnIcon(character);
        }
        else
        {
            base.HitCharacter(character);
        }
    }

    private void SpawnText(Character character)
    {
        float height = GameManager.Instance.gameConfig.textHeight;
        var dd = PoolManager.Instance.SpawnObject(PoolType.TextDisplay).GetComponent<TextDisplay>();
        dd.ShowOneHit();
        //dd.transform.SetParent(GameManager.Instance.gameConfig.textParent);
        dd.transform.position = character.transform.position + height * Vector3.up;
        dd.transform.DOMoveY(dd.transform.position.y + height, 0.5f).OnComplete(() =>
        {
            PoolManager.Instance.DespawnObject(dd.transform);
        });
    }

}
