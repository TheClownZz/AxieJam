using UnityEngine;

public class PosionWard : HealingWard
{
    [SerializeField] Collider2D col2d;
    [SerializeField] float slowTime = 0.5f;
    [SerializeField] float slowRate = 0.5f;
    protected override void OnActive()
    {
        col2d.enabled = true;
        GameManager.Instance.DelayedCall(cooldown / 2, () =>
        {
            col2d.enabled = false;
        });
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.GetComponent<Enemy>();
        if (e)
        {
            SlowEffect effect = new SlowEffect(slowTime, slowRate);
            e.GetCom<EffectController>().AddEffect(effect);
            e.TakePosionDamage(activeValue);
        }
    }


}
