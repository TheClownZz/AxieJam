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

    public virtual void OnSelect()
    {

    }

    public virtual void OnUnSelect()
    {

    }

    public virtual void Clear()
    {

    }
}
