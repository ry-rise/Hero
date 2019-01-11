using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private WallHitter wallhitter;

    // Use this for initialization
    void Start()
    {
        wallhitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector2(0, Speed * Time.fixedDeltaTime));

        if (wallhitter.IsHit(gameObject, HitPointFlag.Bottom | HitPointFlag.Top))
        {
            Destroy(gameObject);
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
                if (result) Destroy(gameObject);
            }
            if (Target.gameObject.transform.root.tag == "Ball")
            {
                Destroy(gameObject);
            }
        }
    }
}
