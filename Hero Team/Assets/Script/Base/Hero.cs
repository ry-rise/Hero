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
    private Vector2 moveVector;
    private WallHitter wallHitter;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        goddess = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        wallHitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        isStarted = false;
        moveVector = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        TypeChange(false);
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
            moveVector = new Vector2(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad)) * speed;
            rb.velocity = moveVector;
        }
        else
        {
            if (transform.parent == null)
            {
                transform.parent = goddess.transform;
            }
        }
    }

    private void Moving()
    {
        WallHitter.HitPointFlag hitPointFlag = wallHitter.IsHit(gameObject);
        /*
        if((hitPointFlag & WallHitter.HitPointFlag.Left) == WallHitter.HitPointFlag.Left)
        {

        }
        if ((hitPointFlag & WallHitter.HitPointFlag.Right) == WallHitter.HitPointFlag.Right)
        {
        }
        if ((hitPointFlag & WallHitter.HitPointFlag.Top) == WallHitter.HitPointFlag.Top)
        {
        }*/
        if ((hitPointFlag & WallHitter.HitPointFlag.Bottom) == WallHitter.HitPointFlag.Bottom)
        {
            FallOut();
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

    //貫通弾に変える
    public void TypeChange(bool IsPenetrated)
    {
        if (IsPenetrated)
        {
            gameObject.layer = LayerMask.NameToLayer("PenetratBall");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Ball");
        }
    }

    private void FallOut()
    {
        TypeChange(false);
    }
}
