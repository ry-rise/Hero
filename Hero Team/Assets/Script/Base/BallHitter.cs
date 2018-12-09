using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitter : MonoBehaviour {

    private Goddess goddess;
    // Use this for initialization
    void Start ()
    {
        goddess = transform.parent.GetComponent<Hero>().goddess;
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            goddess.SmashCounter(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    }
}
