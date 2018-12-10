using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private Goddess goddess;
    [SerializeField]
    private List<Vector2> scales;
    private int scaleLevel;
    public int ScaleLevel
    {
        get
        {
            return scaleLevel;
        }
        set
        {
            scaleLevel = value;
            if (scaleLevel < 0)
            {
                scaleLevel = 0;
            }
            else if (scaleLevel > scales.Count)
            {
                scaleLevel = scales.Count - 1;
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        transform.localScale = scales[scaleLevel] / goddess.transform.lossyScale;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Damage(int value)
    {
        scaleLevel += value;
        transform.localScale = scales[scaleLevel] / goddess.transform.lossyScale;
    }

    public void Heal(int value)
    {
        scaleLevel -= value;
        transform.localScale = scales[scaleLevel] / goddess.transform.lossyScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            goddess.Smashing(collision.gameObject);
        }
    }
}
