using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonTail : MonoBehaviour
{
    private DragonMove dragon;
    // Use this for initialization
    void Start()
    {
        dragon = transform.root.GetComponent<DragonMove>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (dragon.enabled)
            {
                dragon.TailOut();
            }
        }
    }
}