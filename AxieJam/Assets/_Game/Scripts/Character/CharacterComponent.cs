using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponent : MonoBehaviour
{
    [HideInInspector] public Character control;

    public virtual void OnInits(Character control)
    {
        this.control = control;
    }
    public virtual void OnUpdate(float dt)
    {

    }


    public virtual void OnDead()
    {

    }

    public virtual void OnCompleteLevel()
    {

    }

    public virtual void OnLose()
    {

    }

    public virtual void OnStartLevel()
    {

    }

    public virtual void Clear()
    {

    }
}
