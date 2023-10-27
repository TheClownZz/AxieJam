
using System.Collections.Generic;
using UnityEngine;

public class GreenMine : EnemyBullet
{
    [SerializeField] List<GameObject> objRenderList;
    [SerializeField] ParticleSystem fx;
    [SerializeField] AudioClip explosionClip;

    public override void OnInits(Weapon weapon, float speed, Vector3 dir)
    {
        Debug.LogError("OnInits");
        base.OnInits(weapon, speed, dir);
        GameManager.Instance.DelayedCall(1.5f, Clear);
        foreach (GameObject obj in objRenderList)
        {
            obj.SetActive(true);
        }
    }
    protected override void Update()
    {
    }

    public override void Clear()
    {
        Debug.LogError("Clear 2");

        fx.Play();
        SetCol(false);
        AudioManager.Instance.PlaySound(explosionClip);
        foreach(GameObject obj in objRenderList)
        {
            obj.SetActive(false);
        }
        GameManager.Instance.DelayedCall(1, () =>
        {
            base.Clear();
        });
    }
}
