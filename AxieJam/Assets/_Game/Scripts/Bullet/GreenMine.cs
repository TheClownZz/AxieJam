
using System.Collections.Generic;
using UnityEngine;

public class GreenMine : EnemyBullet
{
    [SerializeField] List<GameObject> objRenderList;
    [SerializeField] ParticleSystem fx;
    [SerializeField] AudioClip clip;
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
        fx.Play();
        SetCol(false);
        AudioManager.Instance.PlaySound(clip);
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
