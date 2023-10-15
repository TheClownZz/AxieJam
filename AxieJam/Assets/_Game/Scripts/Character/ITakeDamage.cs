using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage 
{
    public float TakeDamage(float damage, bool isCrit);
}
