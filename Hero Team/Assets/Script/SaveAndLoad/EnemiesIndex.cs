using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemiesIndex : ScriptableObject
{
    [SerializeField, Tooltip("ここに記録していない敵は配置が保存されないので注意")]
    private List<GameObject> enemies;
    public List<GameObject> Enemies { get { return enemies; } }
}