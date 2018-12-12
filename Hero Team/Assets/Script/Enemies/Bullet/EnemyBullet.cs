using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float Speed;
    private float X;
    private float Y;
    private WallHitter wallhitter;

    // Use this for initialization
    void Start()
    {
        wallhitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
    }

    // Update is called once per frame
    void Update()
    {
        X = this.transform.position.x;
        Y = this.transform.position.y;
        this.transform.position = new Vector3(X, Y - Speed * Time.deltaTime, 0);

        if (wallhitter.IsHit(gameObject, HitPointFlag.Bottom))
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D Target)
    {
        if (Target.gameObject.tag == "Bar")
        {
            Target.GetComponent<Bar>().Damage(1);
            Destroy(this.gameObject);
        }
    }
}
