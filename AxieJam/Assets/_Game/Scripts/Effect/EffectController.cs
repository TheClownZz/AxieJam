using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    None,
    Stun,
}
[System.Serializable]
public class EffectBase
{
    public EffectType effectType;
    public float timeActive;
    public float duration;
    public virtual void Active(Character c)
    {
        timeActive = Time.time;
    }
    public virtual void Deactive(Character c) { }

    public virtual void StackEffect(float newActiveTime, float newDuraction)
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
            effectList[index].StackEffect(Time.time, effect.duration);
        }
        else
        {
            effect.Active(control);
            effectList.Add(effect);
        }

    }
}
