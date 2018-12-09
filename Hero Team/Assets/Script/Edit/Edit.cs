using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//なるべく合屋以外が弄らないようにしてください
//勝手に弄ると私が怒ります
[CreateAssetMenu]
public class Edit : ScriptableObject
{
    [SerializeField, Tooltip("射出の初期方向")]
    private int startAngle;
    public int StartAngle { get { return startAngle; } }
    [SerializeField, Tooltip("勇者の初期位置")]
    private Vector2 firstPosition;
    public Vector2 FirstPosition { get { return firstPosition; } }
    [SerializeField, Tooltip("スマッシュカウント")]
    private int smashCount;
    public int SmashCount { get { return smashCount; } }
}