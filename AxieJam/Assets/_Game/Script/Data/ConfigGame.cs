using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CharacterConfig
{
    public float hp;
    public float damage;
    public float armor;
    public float attackSpeed;

}

[System.Serializable]

public class PlayerConfig : CharacterConfig
{
    public float lifeSteal;
    public float dodge;
    public float regen;
    public float critRate;
    public float critDamage;
    [Header("Player name")]
    public string name;

}

[CreateAssetMenu(menuName = "Game/PlayerAsset", fileName = "PlayerAsset")]
public class PlayerAsset : GameAsset
{
    public PlayerConfig data;

}
[CreateAssetMenu(menuName = "Game/ConfigGame", fileName = "ConfigGame")]

public class ConfigGame : ScriptableObject
{

}
