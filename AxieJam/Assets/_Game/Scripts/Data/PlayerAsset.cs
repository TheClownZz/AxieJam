using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerConfig
{
    public string name;
    public Sprite avatar;

    public float hp;
    public float damage;
    public float armor;
    public float attackSpeed;
    public float regen;
    public float moveSpeed;
    public float critRate;
    public float critDamage;
    public float dodge;
    public float lifeSteal;

    public List<float> chartRationList;
}


[CreateAssetMenu(menuName = "Game/PlayerAsset", fileName = "PlayerAsset")]
public class PlayerAsset : GameAsset
{
    public List<PlayerConfig> dataList;

}
