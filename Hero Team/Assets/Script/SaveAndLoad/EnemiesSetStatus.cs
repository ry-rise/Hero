using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesSetStatus
{
    //名前
    public string EnemyName { get; protected set; }
    //座標
    public Vector2 Position { get; protected set; }

    public EnemiesSetStatus(string enemyName, Vector2 position)
    {
        EnemyName = enemyName;
        Position = position;
    }
}
