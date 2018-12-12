using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : BaseEnemyMove
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Direction startDirection;
    private int direction;
    private WallHitter wallhitter;

    enum Direction
    {
        Right = -1,
        Left = 1
    }

    private void Start()
    {
        wallhitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        direction = (int)startDirection;
    }

    private void FixedUpdate()
    {
        if (wallhitter.IsHit(gameObject, HitPointFlag.Right | HitPointFlag.Left))
        {
            direction *= -1;
        }
        transform.Translate(speed * Time.fixedDeltaTime * direction, 0, 0);
    }
}