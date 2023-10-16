using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyConfig
{
    public float hp;
    public float damage;
    public float moveSpeed;
    public float attackSpeed = 0.5f;
    public float armor = 0;
}

[CreateAssetMenu(menuName = "Game/EnemyAsset", fileName = "EnemyAsset")]
public class EnemyAsset : GameAsset
{
    public EnemyConfig data;
}
