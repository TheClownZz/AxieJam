using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack 
{
    public Transform GetTarget();

    public void OnWeaponUpdate();
}
