using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private WallHitter wallhitter;
    public bool IsStoped { get; set; }

    // Use this for initialization
    void Start()
    {
        wallhitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Add(this);
        IsStoped = false;
    }
    private void FixedUpdate()
    {
        if (!IsStoped) {
            transform.Translate(new Vector2(0, Speed * Time.fixedDeltaTime));

            if (wallhitter.IsHit(gameObject, HitPointFlag.Bottom | HitPointFlag.Top))
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

    void OnTriggerEnter2D(Collider2D Target)
    {
        if (gameObject.tag == "EnemyBullet")
        {
            if (Target.gameObject.tag == "Bar")
            {
                bool result = Target.GetComponent<Bar>().Damage(1, this);
                if (result)
                {
                    GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Remove(this);
                    Destroy(gameObject);
                }
            }
            if (Target.gameObject.transform.root.tag == "Ball")
            {
                GameObject.Find("EnemyManager").GetComponent<EnemyManager>().Bullets.Remove(this);
                Destroy(gameObject);
            }
        }
    }
}
