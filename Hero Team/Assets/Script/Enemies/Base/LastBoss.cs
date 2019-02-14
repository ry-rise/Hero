using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBoss : BaseEnemy
{
    [SerializeField]
    private BaseEnemyAttack[] attacks;
    [SerializeField]
    private ObjectEnemy[] parts;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void AttackSwitch()
    {
        if (attack == null) return;
        if ((Stop & StopStatus.AttackStoped) != StopStatus.AttackStoped)
        {
            if (!attack.enabled)
            {
                attack.enabled = true;
                foreach (BaseEnemyAttack it in attacks)
                {
                    it.enabled = true;
                }
            }
        }
        else
        {
            if (attack.enabled)
            {
                attack.enabled = false;
                foreach (BaseEnemyAttack it in attacks)
                {
                    it.enabled = false;
                }
            }
        }
    }

    protected override void Die()
    {
        foreach (ObjectEnemy it in parts)
        {
            if (it != null)
            {
                it.IsDead();
            }
        }
        base.Die();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
