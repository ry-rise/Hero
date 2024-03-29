﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField]
    private Goddess goddess;
    [SerializeField]
    private float limitAngle;
    [SerializeField]
    private SpriteRenderer GoddessImage;
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
            goddess.transform.localScale = scales[ScaleLevel];
        }
    }

    public int MaxScaleLevel { get { return scales.Count - 1; } }

    public bool IsCounterAttacked { get; set; }

    // Use this for initialization
    void Start ()
    {
        IsCounterAttacked = false;
        goddess.transform.localScale = scales[ScaleLevel];
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public bool Damage(int value, EnemyBullet bullet = null)
    {
        if (!IsCounterAttacked)
        {
            ScaleLevel += value;
            return true;
        }
        else
        {
            bullet.ReturnMove();
            return false;
        }
    }

    public void Heal(int value)
    {
        ScaleLevel -= value;
    }

    public void ResetScale()
    {
        ScaleLevel = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            goddess.Swing();
            ++GameManager.BarHitCount;
            Hero ball = collision.gameObject.GetComponent<Hero>();
            Vector2 ver = collision.transform.root.position - transform.position;
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(ver.x * (limitAngle / 180), ver.y) * ball.Speed;
        }
    }
}
