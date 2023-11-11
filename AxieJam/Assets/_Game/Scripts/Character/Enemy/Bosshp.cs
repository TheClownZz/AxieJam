
using UnityEngine;
using UnityEngine.UI;


public class Bosshp : EnemyHp
{
    [SerializeField] Image hpSlider;
    float maxhp;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        maxhp = control.stat.hp;
        hpSlider.fillAmount = 1;
        hpSlider.transform.parent.gameObject.SetActive(true);
    }

    public override void OnDead()
    {
        base.OnDead();
        hpSlider.transform.parent.gameObject.SetActive(false);
    }

    public override float TakeDamage(float damage, bool isCrit)
    {
        float result = base.TakeDamage(damage, isCrit);
        hpSlider.fillAmount = currentHp / maxhp;
        return result;
    }
}


