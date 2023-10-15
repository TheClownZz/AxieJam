using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class EffectData
{
    public EffectType effectType;
    public float duration;
    public float rate = 1.1f;
}

[CreateAssetMenu(menuName = "Game/EffectAsset", fileName = "EffectAsset")]
public class EffectAsset : GameAsset
{
    public List<EffectData> effectDataList;
}
