using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : BaseEnemyMove
{
    [SerializeField]
    private float speed;

    private void FixedUpdate()
    {
        transform.Translate(0, speed * Time.fixedDeltaTime, 0);
    }
}