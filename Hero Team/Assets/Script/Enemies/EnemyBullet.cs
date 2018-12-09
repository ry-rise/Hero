using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    [SerializeField]
    private float Speed;
    private float X;
    private float Y;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        X = this.transform.position.x;
        Y = this.transform.position.y;
        this.transform.position = new Vector3(X, Y - Speed * Time.deltaTime ,0);

        if(transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

	}

    void OnTriggerEnter2D(Collider2D Target)
    {

    
        if (Target.gameObject.tag == "Bar" )
        {
           
            Destroy(this.gameObject);

        }
     
    }
}
