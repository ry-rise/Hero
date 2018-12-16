using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesShowStatus
{
    //名前
    public string EnemyName;
    //座標
    public Vector2 Position;

    public EnemiesShowStatus(string enemyName, Vector2 position)
    {
        EnemyName = enemyName;
        Position = position;
    }
}
