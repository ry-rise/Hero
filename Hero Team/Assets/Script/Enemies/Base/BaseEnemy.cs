using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    protected EnemyManager manager;
    protected BaseEnemyMove move;
    protected BaseEnemyAttack attack;
    [SerializeField]
    private int HP = 1;
    [SerializeField]
    private int Score = 1;
    // Use this for initialization
    [SerializeField]
    private GameObject dropItem;
    public GameObject DropItem { get { return dropItem; } }
    [SerializeField]
    private GameObject sePrefab;

    public void FirstSetting(EnemiesSetStatus table)
    {
        dropItem = table.DropItem;
    }

    public enum StopStatus
    {
        None = 0,   //全て実行
        MoveStoped = 1 << 0,    //移動を止める
        AttackStoped = 1 << 1,  //攻撃を止める
        ALL = MoveStoped + AttackStoped
    }

    public StopStatus stop;

    virtual protected void MoveSwitch()
    {
        if (move == null) return;
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

    virtual protected void AttackSwitch()
    {
        if (attack == null) return;
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
            Die();
        }
    }

    virtual protected void Die()
    {
        if (DropItem != null)
        {
            Instantiate(DropItem, transform.position, Quaternion.identity);
        }
        GameManager.EnemyScore += Score;
        manager.Enemies.Remove(this);

        Destroy(gameObject);
    }
    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            HP -= collision.transform.root.GetComponent<Hero>().Power;
            if (sePrefab != null)
            {
                Instantiate(sePrefab, transform.position, Quaternion.identity);
            }
        }
        else if (collision.gameObject.transform.root.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            HP -= 1;
        }
    }
}