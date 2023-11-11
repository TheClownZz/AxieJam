using UnityEngine;

public class BossAttack : EnemyAttack
{
    protected int skillIndex;
    [SerializeField] protected AudioGetter attackClipGetter;

    public void SetIndex(int index)
    {
        this.skillIndex = index;
    }
}
