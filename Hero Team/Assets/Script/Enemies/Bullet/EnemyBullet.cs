using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    protected float Speed;
    protected WallHitter wallhitter;
    public bool IsStoped { get; set; }
    [SerializeField]
    protected GameObject sePrefab;

    // Use this for initialization
    void Start()
    {
        wallhitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Add(this);
        IsStoped = false;
    }
    protected void FixedUpdate()
    {
        if (!IsStoped) {
            transform.Translate(new Vector2(0, Speed * Time.fixedDeltaTime));

            if (wallhitter.IsHit(gameObject, HitPointFlag.Bottom))
            {
                GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Remove(this);
                Destroy(gameObject);
            }
        }
    }

    public void ReturnMove()
    {
        Speed *= -1;
        gameObject.tag = "PlayerBullet";
    }

    virtual protected void OnTriggerEnter2D(Collider2D collision)
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
                GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}
