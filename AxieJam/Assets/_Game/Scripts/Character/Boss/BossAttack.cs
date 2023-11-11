using UnityEngine;

public class BossAttack : EnemyAttack
{
    protected int skillIndex;
    protected AudioClip attackClip;
    [SerializeField] AssetGetter audioGetter;
    protected virtual void Awake()
    {
        audioGetter.OnGetAsset = (audio) =>
        {
            attackClip = (AudioClip)audio;
        };
        audioGetter.LoadAsset();
    }
    public void SetIndex(int index)
    {
        this.skillIndex = index;
    }
}
