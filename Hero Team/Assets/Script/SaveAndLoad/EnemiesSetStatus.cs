﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesSetStatus
{
    //名前
    [SerializeField]
    private string enemyName;
    public string EnemyName { get { return enemyName; } set { enemyName = value; } }
    //座標
    [SerializeField]
    private Vector2 position;
    public Vector2 Position { get { return position; } set { position = value; } }
    //ドロップアイテム
    [SerializeField]
    private GameObject dropItem;
    public GameObject DropItem { get { return dropItem; } set { dropItem = value; } }

    public EnemiesSetStatus(string enemyName, Vector2 position, GameObject dropItem)
    {
        EnemyName = enemyName;
        Position = position;
        DropItem = dropItem;
    }
}
