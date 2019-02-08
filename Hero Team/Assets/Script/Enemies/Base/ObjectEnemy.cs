using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnemy : BaseEnemy
{
    protected override void Awake()
    {
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        manager.Enemies.Add(this);
        move = GetComponent<BaseEnemyMove>();
        attack = GetComponent<BaseEnemyAttack>();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void IsDead()
    {
        Die();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
