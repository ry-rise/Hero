using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private bool isStarted;
    private WallHitter wallHitter;
    private Rigidbody2D rb;
    [SerializeField]
    private float penetratTime;
    private float penetratTimeCount;
    private bool isPenetrated;
    private PlayerManager manager;
    [SerializeField]
    private StatusEdit status;
    public float Speed { get { return status.Speed; } }
    public int Power { get { return status.Power; } }   //勇者の攻撃力

    private Vector2 nowVelocity;

    private bool isStoped;
    public bool IsStoped
    {
        get { return isStarted; }
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
            isStarted = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        manager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
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

    private void Update()
    {
        //Debug用
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TypeChange(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TypeChange(true);
        }
    }

    public void StartGame()
    {
        isStarted = true;
        transform.parent = null;
        SetSpeed(true);
        TypeChange(false);
    }

    private void Moving()
    {
        SetSpeed();
        PenetratCounter();
        //落下したら
        if (wallHitter.IsHit(gameObject, HitPointFlag.Bottom))
        {
            FallOut();
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
        TypeChange(true);
        isStarted = false;
    }
    //貫通
    private void PenetratCounter()
    {
        if (isPenetrated)
        {
            if (penetratTimeCount < penetratTime)
            {
                penetratTimeCount += Time.fixedDeltaTime;
            }
            else
            {
                TypeChange(false);
            }
        }
    }

    //貫通弾に変える
    public void TypeChange(bool IsPenetrated)
    {
        isPenetrated = IsPenetrated;
        if (IsPenetrated)
        {
            penetratTimeCount = 0;
            gameObject.layer = LayerMask.NameToLayer("PenetratBall");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Ball");
        }
    }

    private void FallOut()
    {
        manager.Balls.Remove(this);
        Destroy(gameObject);
    }
}
