using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{

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
        transform.localScale = scales[scaleLevel];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Damage(int value)
    {
        scaleLevel += value;
        transform.localScale = scales[scaleLevel];
    }

    public void Heal(int value)
    {
        scaleLevel -= value;
        transform.localScale = scales[scaleLevel];
    }
}
