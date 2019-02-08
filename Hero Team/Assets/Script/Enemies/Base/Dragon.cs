﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : BaseEnemy
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    override protected void MoveSwitch()
    {
        if (move == null) return;
        if ((stop & StopStatus.MoveStoped) != StopStatus.MoveStoped)
        {
            if (!move.enabled)
            {
                ((DragonMove)move).AnimationFlagChanger(true);
                move.enabled = true;
            }
        }
        else
        {
            if (move.enabled)
            {
                ((DragonMove)move).AnimationFlagChanger(false);
                move.enabled = false;
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
