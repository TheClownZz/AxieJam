using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGun : PlayerGun
{
    [SerializeField] PosionWard ward;
    public override void ActiveSKill(SkillConfig config)
    {
        base.ActiveSKill(config);

        float range = config.GetSkillValue(SkillType.Range, 12);
        float coolDown = config.GetSkillValue(SkillType.Cooldown, 0.2f);
        float activeValue = config.GetSkillValue(SkillType.ActiveValue, 0.15f);

        ward.transform.SetParent(GameManager.Instance.objMap.transform.GetChild(0), false); // map chid 0 is destroyler

        ward.transform.position = transform.position;
        ward.transform.localScale = Vector3.one * range;
        ward.OnInits(activeValue * GetCharacterStat().damage, coolDown, config.defaultValue.duration);
    }
}
