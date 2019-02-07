using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SowrdAttack : MonoBehaviour {
    [SerializeField]
    private GameObject Slash;
    public Vector3 SlashPoint;
    private BossEnemy bossEnemy;
    private IEnumerator DelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
         
        this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, 150) ;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.tag == "Ball")
            {
                    
            this.gameObject.transform.rotation = Quaternion.Euler(this.gameObject.transform.rotation.x, this.gameObject.transform.rotation.y, 250);
            Instantiate(Slash, SlashPoint, Quaternion.Euler(0, 0, 85));
                StartCoroutine("DelayMethod", 0.3f);
            }
        }
    
}
