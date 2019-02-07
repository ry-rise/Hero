using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBullet : EnemyBullet
{
    [SerializeField]
    private int hp = 2;
    private bool hit;

    override protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "EnemyBullet")
        {
            if (collision.tag == "Bar")
            {
                bool result = collision.GetComponent<Bar>().Damage(1, this);
                if (result)
                {
                    GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Remove(this);
                    Instantiate(sePrefab, transform.position, Quaternion.identity);
                    Destroy(gameObject);
                }
            }
            if (!hit && collision.tag == "Ball")
            {
                --hp;
                if (hp < 1)
                {
                    GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Remove(this);
                    Destroy(gameObject);
                }
                hit = true;
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ball")
        {
            hit = false;
        }
    }
}
