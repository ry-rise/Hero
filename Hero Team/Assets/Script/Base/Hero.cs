using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private GameObject goddess;
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

    private Vector2 firstPosition;

    // Use this for initialization
    void Start()
    {
        goddess = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        wallHitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        rb = GetComponent<Rigidbody2D>();
        Setting(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isStarted)
        {
            StartWaitting();
        }
        else
        {
            Moving();
        }
    }

    private void StartWaitting()
    {
        if (controller.State == InputController.Status.Released)
        {
            isStarted = true;
            transform.parent = null;
            rb.velocity = new Vector2(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad)) * speed;
        }
    }

    private void Moving()
    {
        PenetratCounter();
        //壁の当たり判定をとってくる
        WallHitter.HitPointFlag hitPointFlag = wallHitter.IsHit(gameObject);
        //落下したら
        if ((hitPointFlag & WallHitter.HitPointFlag.Bottom) == WallHitter.HitPointFlag.Bottom)
        {
            FallOut();
        }
    }

    private void Setting(bool isSettedFirst = false)
    {
        rb.velocity = Vector2.zero;
        transform.parent = goddess.transform;
        TypeChange(false);
        isStarted = false;
        if (isSettedFirst)
        {
            firstPosition = transform.localPosition;
        }
        else
        {
            transform.localPosition = firstPosition;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TypeChange(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TypeChange(true);
        }
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
        Setting();
    }
}
