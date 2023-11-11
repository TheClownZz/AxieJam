using UnityEngine;

public class BossAttack : EnemyAttack
{
    protected int skillIndex;
    [SerializeField] protected AudioClip attackClip;
    [SerializeField] AssetGetter audioGetter;

    private void Awake()
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
