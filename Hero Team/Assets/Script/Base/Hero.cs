using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private InputController controller;
    private bool isStarted;
    [SerializeField]
    private float startAngle;
    private WallHitter wallHitter;
    private Rigidbody2D rb;
    [SerializeField]
    private float penetratTime;
    private float penetratTimeCount;
    private bool isPenetrated;
    public Goddess goddess { get; private set; }
    [SerializeField]
    private StatusEdit status;
    public float Speed { get { return status.Speed; } }
    public int Power { get { return status.Power; } }   //勇者の攻撃力

    // Use this for initialization
    void Awake()
    {
        goddess = GameObject.FindGameObjectWithTag("Player").GetComponent<Goddess>();
        goddess.Balls.Add(this);
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        wallHitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        rb = GetComponent<Rigidbody2D>();
        Setting();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStarted)
        {
            Moving();
        }
    }

    private void Update()
    {
        if (!isStarted)
        {
            StartGame();
        }
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

    private void StartGame()
    {
        if (controller.State == InputController.Status.Pushed)
        {
            isStarted = true;
            transform.parent = null;
            SetSpeed(true);
        }
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
            rb.velocity = new Vector2(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad)) * Speed;
        }
        else
        {
            if (Vector2.zero != rb.velocity)
            {
                float scalar = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y);
                Vector2 unitVector = new Vector2(rb.velocity.x / scalar, rb.velocity.y / scalar);
                rb.velocity = unitVector * Speed;
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad)) * Speed;
            }
        }
    }

    private void Setting()
    {
        rb.velocity = Vector2.zero;
        TypeChange(false);
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
        goddess.Balls.Remove(this);
        Destroy(gameObject);
    }
}
