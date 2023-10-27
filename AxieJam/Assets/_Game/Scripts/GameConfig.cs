using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    public const int maxPlayer = 3;
    public const int maxlevel = 5;

    public float forceBackTime = 0.5f;
    [SerializeField, Range(0, 5)]
    public float forceDrag = 1f;
    public float forceValue = 50;

    [Space(5)]
    public float textHeight = 0.5f;
    public float iconHeight = 0.5f;
    [Space(5)]
    public float spawnRadius = 5;
    [Space(5)]
    public Color critColor;

    public float normalAnimSacle = 1.5f;
    public float deadAnimScale = 2f;
}
