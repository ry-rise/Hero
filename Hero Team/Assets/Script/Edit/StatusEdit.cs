using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//なるべく合屋以外が弄らないようにしてください
//勝手に弄ると私が怒ります
[CreateAssetMenu]
public class StatusEdit : ScriptableObject
{
    [SerializeField, Tooltip("当たり判定の範囲")]
    private float hitSize;
    public float HitSize { get { return hitSize; } }
    [SerializeField, Tooltip("持続時間")]
    private float limitTime;
    public float LimitTime { get { return limitTime; } }
    [SerializeField, Tooltip("ゲージの上昇量")]
    private int chargeAmount;
    public int ChargeAmount { get { return chargeAmount; } }
    [SerializeField, Tooltip("攻撃力")]
    private int power;
    public int Power { get { return power; } }
    [SerializeField, Tooltip("速度")]
    private int speed;
    public int Speed { get { return speed; } }
}