using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBullet : EnemyBullet
{
    public int BulletHp = 2;
    [SerializeField]
    private GameObject sePrefab;

    private void OnTriggerExit2D (Collider2D collision)
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
            if (collision.tag == "Ball")
            {
                BulletHp--;
                if (BulletHp < 1)
                {
                    GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Remove(this);
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
