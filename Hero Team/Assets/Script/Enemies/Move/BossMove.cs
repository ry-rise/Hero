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
    [SerializeField]
    Sprite[] BossSprite;
    [SerializeField]
    SpriteRenderer BossImage;

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
            if(BossImage.sprite == BossSprite[0])
            {
                BossImage.sprite = BossSprite[1];
            }
            else
            {
                BossImage.sprite = BossSprite[0];
            }
         
        }
        transform.Translate(speed * Time.fixedDeltaTime * direction, 0, 0);
    }
}