using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private Goddess goddess;
    [SerializeField]
    private float limitAngle;
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
            else if (scaleLevel >= scales.Count)
            {
                scaleLevel = scales.Count - 1;
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        transform.localScale = scales[ScaleLevel] / goddess.transform.lossyScale;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public void Damage(int value)
    {
        ScaleLevel += value;
        transform.localScale = scales[ScaleLevel] / goddess.transform.lossyScale;
    }

    public void Heal(int value)
    {
        ScaleLevel -= value;
        transform.localScale = scales[ScaleLevel] / goddess.transform.lossyScale;
    }

    public void ResetScale()
    {
        ScaleLevel = 0;
        transform.localScale = scales[ScaleLevel] / goddess.transform.lossyScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.root.tag == "Ball")
        {
            if (!goddess.Smashing(collision.transform.root.gameObject))
            {
                Hero ball = collision.gameObject.GetComponent<Hero>();
                Vector2 ver = collision.transform.root.position - transform.position;

                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(ver.x * (limitAngle / 180), ver.y) * ball.Speed;
                Debug.Log(Mathf.Atan2(ball.GetComponent<Rigidbody2D>().velocity.y, ball.GetComponent<Rigidbody2D>().velocity.x) *Mathf.Rad2Deg);
            }
        }
    }
}
