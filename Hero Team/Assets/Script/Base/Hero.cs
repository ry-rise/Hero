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
    private Vector2 moveVector;

    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        isStarted = false;
        moveVector = Vector2.zero;
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
            moveVector = new Vector2(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad)) * speed;
            isStarted = true;
        }
    }

    void Moving()
    {
        transform.Translate(moveVector * Time.deltaTime);
    }
}
