using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//なるべく合屋以外が弄らないようにしてください
//勝手に弄ると私が怒ります
[CreateAssetMenu]
public class StatusEdit : ScriptableObject
{
    [SerializeField, Tooltip("攻撃力")]
    private int power;
    public int Power { get { return power; } }
}