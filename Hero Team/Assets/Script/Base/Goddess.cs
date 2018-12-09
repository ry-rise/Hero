using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goddess : MonoBehaviour {

    private InputController controller;
    [SerializeField]
    private Vector2 firstPosition;
    public List<Hero> Balls;
    [SerializeField]
    private GameObject ballPrefab;

    // Use this for initialization
    private void Awake()
    {
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
    }

    private void Start()
    {
        if (Balls.Count == 0)
        {
            BallSet(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (controller.State == InputController.Status.Pressing || controller.State == InputController.Status.Pushed)
        {
            transform.position = new Vector2(controller.TouchMovePoint.x, transform.position.y);
        }
        if (Balls.Count == 0)
        {
            BallSet();
        }
    }

    void BallSet(bool isStarted = false)
    {
        Instantiate(ballPrefab, firstPosition, Quaternion.identity);
        if (!isStarted)
        {
            //ライフを減らす処理
        }
    }
}