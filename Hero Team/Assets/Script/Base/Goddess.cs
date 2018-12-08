using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goddess : MonoBehaviour {

    private InputController controller;
    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.State == InputController.Status.Pressing || controller.State == InputController.Status.Pushed)
        {
            transform.position = new Vector2(controller.TouchMovePoint.x, transform.position.y);
        }
    }
}