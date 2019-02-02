using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField]
    private SeManager se;
    private bool isStarted;
    private WallHitter wallHitter;
    private Rigidbody2D rb;
    private PlayerManager manager;
    private StatusEdit status;
    public float HitSize { get { return status.HitSize; } }
    public int ChargeAmount { get { return status.ChargeAmount; } }
    public float Speed { get { return status.Speed; } }
    public int Power { get { return status.Power; } }   //勇者の攻撃力

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
            }
            else
            {
                rb.velocity = nowVelocity;
            }
            isStoped = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        manager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        status = manager.Statuses[GameManager.SmashLevel];
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
                rb.velocity = unitVector * Speed;
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
        if (collision.gameObject.tag == "Enemy")
        {
            manager.SmashCounter(ChargeAmount);
        }
    }
}
