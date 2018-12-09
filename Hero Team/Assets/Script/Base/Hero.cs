using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private InputController controller;
    private bool isStarted;
    [SerializeField]
    private float startAngle;
    [SerializeField]
    private float speed;
    private WallHitter wallHitter;
    private Rigidbody2D rb;
    [SerializeField]
    private float penetratTime;
    private float penetratTimeCount;
    private bool isPenetrated;
    private Goddess goddess;

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
            rb.velocity = new Vector2(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad)) * speed;
        }
    }

    private void Moving()
    {
        PenetratCounter();
        //落下したら
        if (wallHitter.IsHit(gameObject, WallHitter.HitPointFlag.Bottom))
        {
            FallOut();
        }
    }

    private void Setting()
    {
        rb.velocity = Vector2.zero;
        TypeChange(false);
        isStarted = false;
    }

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
