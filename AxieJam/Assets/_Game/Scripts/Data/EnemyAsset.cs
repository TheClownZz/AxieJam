using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyConfig
{
    public float hp;
    public float armor;
    public float damage;
    public float critRate;
    public float moveSpeed;
    public float critDamage;
    public float attackSpeed = 0.5f;
}

[CreateAssetMenu(menuName = "Game/EnemyAsset", fileName = "EnemyAsset")]
public class EnemyAsset : GameAsset
{
    public EnemyConfig data;
}
