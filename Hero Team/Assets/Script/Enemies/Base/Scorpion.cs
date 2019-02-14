using System.Collections.Generic;
using UnityEngine;

public class Scorpion : BaseEnemy
{
    public List<ObjectEnemy> CactusObjects;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void MoveSwitch()
    {
        if (move == null) return;
        if ((Stop & StopStatus.MoveStoped) != StopStatus.MoveStoped)
        {
            if (!move.enabled)
            {
                ((ScorpionMove)move).AnimationFlagChanger(true);
                move.enabled = true;
            }
        }
        else
        {
            if (move.enabled)
            {
                ((ScorpionMove)move).AnimationFlagChanger(false);
                move.enabled = false;
            }
        }
    }

    protected override void Die()
    {
        foreach (ObjectEnemy it in CactusObjects)
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

