using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private InputController controller;
    private bool isStarted;
    [SerializeField]
    private float startAngle;
    private float angle;
    [SerializeField]
    private float speed;
    private Vector2 moveVector;
    private WallHitter wallHitter;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        wallHitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        isStarted = false;
        moveVector = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        angle = startAngle;
    }

    // Update is called once per frame
    void Update()
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

    void StartWaitting()
    {
        if (controller.State == InputController.Status.Released)
        {
            isStarted = true;
            moveVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
            rb.velocity = moveVector;
        }
    }

    void Moving()
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
        }
    }
}
