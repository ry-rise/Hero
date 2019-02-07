using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTail : MonoBehaviour
{
    public bool TailAlive = false;
    public bool TailMove = false;
    BossEnemy bossEnemy;
    
    public float RevivalTime = 0.0f;
    // Use this for initialization
    void Start () {
        bossEnemy = this.transform.root.GetComponent<BossEnemy>();

    }
	
	// Update is called once per frame
	 void Update () {
        if (bossEnemy.stop == 0) {
            if (TailAlive && TailMove == false)
            {
                if (this.gameObject.transform.position.x > 0.66)
                {
                    Vector3 vec3 = this.gameObject.transform.position;
                    vec3.x -= 0.05f;
                    this.gameObject.transform.position = vec3;
                }
                else
                {
                    TailMove = true;
                }
            }
            if (TailAlive == false)
            {
                RevivalTime += Time.deltaTime;
                if (RevivalTime > 10)
                {
                    TailAlive = true;
                }
            }
        }
	}
   void OnCollisionEnter2D(Collision2D collision)
    {
        if (TailAlive && TailMove) {
            if (collision.gameObject.tag == "Ball")
            {
                TailAlive = false;
                Vector3 vec3 = new Vector3(7, -1.9f, 0);
                this.gameObject.transform.position = vec3;
                RevivalTime = 0;
            }
        }
    }
}
