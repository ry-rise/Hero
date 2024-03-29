﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private SeManager se;
    [SerializeField]
    private GameObject slashPrefab;
    [SerializeField]
    private GameObject slashPrefab2;
    private bool isStarted;
    private WallHitter wallHitter;
    private Rigidbody2D rb;
    private PlayerManager manager;
    private StatusEdit status;
    public float HitSize { get { return status.HitSize; } }
    public int ChargeAmount { get { return status.ChargeAmount; } }
    public float Speed { get { return status.Speed; } }
    public float SmashSpeed { get { return status.SmashSpeed; } }
    public int Power { get { return status.Power; } }   //勇者の攻撃力
    public bool IsPenetrated { get { return manager.IsPenetrated; } }

    private Vector2 nowVelocity;

    private bool isStoped;
    public bool IsStoped
    {
        get { return isStoped; }
        set
        {
            if (value)
            {
                nowVelocity = rb.velocity;
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                rb.velocity = nowVelocity;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            isStoped = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        manager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        status = manager.Statuses[GameManager.SelectLevel];
        transform.localScale = new Vector2(HitSize, HitSize);
        manager.Balls.Add(this);
        wallHitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        rb = GetComponent<Rigidbody2D>();
        Setting();
        IsStoped = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsStoped)
        {
            if (isStarted)
            {
                Moving();
            }
        }
    }

    public void StartGame()
    {
        isStarted = true;
        transform.parent = null;
        SetSpeed(true);
    }

    private void Moving()
    {
        SetSpeed();
        //落下したら
        if (wallHitter.IsHit(gameObject, HitPointFlag.Bottom))
        {
            FallOut();
        }
        if (wallHitter.IsHit(gameObject, HitPointFlag.Left | HitPointFlag.Right | HitPointFlag.Top))
        {
            se.Play();
        }
    }

    private void SetSpeed(bool isFirsted = false)
    {
        if (isFirsted)
        {
            rb.velocity = new Vector2(Mathf.Cos(manager.StartAngle * Mathf.Deg2Rad), Mathf.Sin(manager.StartAngle * Mathf.Deg2Rad)) * Speed;
        }
        else
        {
            if (Vector2.zero != rb.velocity)
            {
                float scalar = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y);
                Vector2 unitVector = new Vector2(rb.velocity.x, rb.velocity.y) / scalar;
                if (0.9f < unitVector.x)
                {
                    unitVector = new Vector2(0.5f, 0.5f);
                }
                else if (-0.9f > unitVector.x)
                {
                    unitVector = new Vector2(-0.5f, 0.5f);
                }
                rb.velocity = unitVector * (IsPenetrated ? SmashSpeed : Speed);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Cos(manager.StartAngle * Mathf.Deg2Rad), Mathf.Sin(manager.StartAngle * Mathf.Deg2Rad)) * Speed;
            }
        }
    }

    public void ResetPosition()
    {
        Setting();
        transform.position = manager.FirstPosition;
    }

    private void Setting()
    {
        rb.velocity = Vector2.zero;
        isStarted = false;
    }

    private void FallOut()
    {
        manager.Balls.Remove(this);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.root.tag == "Enemy")
        {
            Vector2 a = transform.position;
            Vector2 b = collision.GetContact(0).point;//collision.gameObject.transform.position;
            Vector2 position = new Vector2(b.x - a.x, b.y - a.y) / Mathf.Sqrt((b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y));
            if (IsPenetrated)
            {
                Instantiate(slashPrefab2, a + position * 1.5f, Quaternion.identity);
            }
            else
            {
                Instantiate(slashPrefab, a + position * 1.5f, Quaternion.identity);
            }
            manager.SmashCounter(ChargeAmount);
        }
    }
}
