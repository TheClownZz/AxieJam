using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    None,
    Stun,
    Slow,
}
[System.Serializable]
public class EffectBase
{
    public EffectType effectType;
    public float timeActive;
    public float duration;
    public float activeValue;
    public virtual void Active(Character c)
    {
        timeActive = Time.time;
    }
    public virtual void Deactive(Character c) { }

    public virtual void StackEffect(float newActiveTime, float newDuraction, EffectBase effect)
    {
        float fisnishTime = timeActive + duration;
        float newFisnishTime = newActiveTime + newDuraction;
        if (fisnishTime < newFisnishTime)
        {
            timeActive = newActiveTime;
            duration = newDuraction;
        }
    }
}
public class StunEffect : EffectBase
{
    public StunEffect(float duration)
    {
        this.duration = duration;
        effectType = EffectType.Stun;
    }
    public override void Active(Character c)
    {
        base.Active(c);
        c.DisableEnemy(true);
    }

    public override void Deactive(Character c)
    {
        base.Deactive(c);
        c.DisableEnemy(false);
    }
}

public class SlowEffect : EffectBase
{
    public SlowEffect(float duration, float activeValue)
    {
        this.duration = duration;
        this.activeValue = activeValue;
        effectType = EffectType.Slow;
    }

    public override void StackEffect(float newActiveTime, float newDuraction, EffectBase effect)
    {
        base.StackEffect(newActiveTime, newDuraction, effect);
        effect.activeValue = Mathf.Max(effect.activeValue, activeValue);
    }
    public override void Active(Character c)
    {
        base.Active(c);
        c.GetCom<EnemyMove>().UpdateSpeed(activeValue);
    }

    public override void Deactive(Character c)
    {
        base.Deactive(c);
        c.GetCom<EnemyMove>().UpdateSpeed(1);
    }
}

public class EffectController : CharacterComponent
{
    public List<EffectBase> effectList = new List<EffectBase>();

    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);
        for (int i = effectList.Count - 1; i >= 0; i--)
        {
            if (Time.time - effectList[i].timeActive >= effectList[i].duration)
            {
                effectList[i].Deactive(control);
                effectList.RemoveAt(i);
            }
        }
    }
    public void AddEffect(EffectBase effect)
    {
        int index = effectList.FindIndex(x => x.effectType == effect.effectType);
        if (index != -1)
        {
            effectList[index].StackEffect(Time.time, effect.duration, effect);
        }
        else
        {
            effect.Active(control);
            effectList.Add(effect);
        }

    }
}
