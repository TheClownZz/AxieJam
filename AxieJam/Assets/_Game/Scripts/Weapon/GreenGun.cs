using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGun : PlayerGun
{
    [SerializeField] HealingWard ward;
    public override void ActiveSKill(PlayerSkillConfig config)
    {
        base.ActiveSKill(config);

        float range = config.GetSkillValue(SkillType.Range, 15);
        float coolDown = config.GetSkillValue(SkillType.Cooldown, 0.5f);
        float activeValue = config.GetSkillValue(SkillType.ActiveValue, 0.05f);

        ward.transform.SetParent(GameManager.Instance.objMap.transform.GetChild(0), false); // map chid 0 is destroyler

        ward.transform.position = transform.position;
        ward.transform.localScale = Vector3.one * range;
        ward.OnInits(activeValue, coolDown, config.defaultValue.duration);
    }

}
