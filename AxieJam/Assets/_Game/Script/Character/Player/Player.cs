using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    public bool isActive;
    [SerializeField] PlayerAsset asset;


    public T GetPCom<T>() where T : PlayerComponent
    {
        foreach (var comp in componentList)
            if (comp is T)
                return comp as T;
        return null;
    }
 
    public void StartLevel()
    {
        isDead = false;
        isActive = true;
        foreach (var comp in componentList)
            comp.OnStartLevel();
        SetState(CharacterState.Alive);
        transform.position = Vector3.zero;
    }

    public void OnUpdate(float dt)
    {
        if (!isActive || isDead)
            return;
        foreach (var comp in componentList)
            comp.OnUpdate(dt);
    }

    public override void OnDead()
    {
        base.OnDead();
        foreach (var comp in componentList)
            comp.OnDead();
        SetState(CharacterState.Die);
    }

  

    public override void LifeSteal(float hp)
    {
    }

    public override float TakeDamage(float damage, bool isCrit)
    {
        return 0;
       // return GetPCom<PlayerHp>().TakeDamage(damage, isCrit);
    }

}
