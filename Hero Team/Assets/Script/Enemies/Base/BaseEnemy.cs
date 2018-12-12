﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected EnemyManager manager;
    protected BaseEnemyMove move;
    protected BaseEnemyAttack attack;
    protected WallHitter wallHitter;
    protected GameManager gameManager;
    [SerializeField]
    private int HP = 1;
    // Use this for initialization

    public enum StopStatus
    {
        None = 0,   //全て実行
        MoveStoped = 1 << 0,    //移動を止める
        AttackStoped = 1 << 1,  //攻撃を止める
        ALL = MoveStoped + AttackStoped
    }

    public StopStatus stop;

    private void MoveSwitch()
    {
        if ((stop & StopStatus.MoveStoped) != StopStatus.MoveStoped)
        {
            if (!move.enabled)
            {
                move.enabled = true;
            }
        }
        else
        {
            if (move.enabled)
            {
                move.enabled = false;
            }
        }
    }

    private void AttackSwitch()
    {
        if ((stop & StopStatus.AttackStoped) != StopStatus.AttackStoped)
        {
            if (!attack.enabled)
            {
                attack.enabled = true;
            }
        }
        else
        {
            if (attack.enabled)
            {
                attack.enabled = false;
            }
        }
    }

    virtual protected void Awake()
    {
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        wallHitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.Enemies.Add(this);
        move = GetComponent<BaseEnemyMove>();
        attack = GetComponent<BaseEnemyAttack>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        MoveSwitch();
        AttackSwitch();
        if (HP < 1)
        {
            manager.Enemies.Remove(this);
            Destroy(gameObject);
        }
        if (wallHitter.IsHit(gameObject, HitPointFlag.Bottom))
        {
            //ゲームオーバーを呼ぶ
            //gameManager;
        }
    }

    virtual protected void OnTriggerExit2D(Collider2D target)
    {
        if (target.gameObject.transform.root.tag == "Ball")
        {
            HP -= target.transform.root.GetComponent<Hero>().Power;
        }
    }
}