using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartImage : MonoBehaviour
{
    private InputController controller;

    Status state;
    private Image image;
    //private Text text;
    //private string waitText;

    enum Status
    {
        Waiting,
        Starting
    }

    void Start()
    {
        image = GetComponent<Image>();
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        state = Status.Waiting;
    }

    void Update()
    {
        switch (state)
        {
            case Status.Waiting:
                Waiting();
                break;
            case Status.Starting:
                Starting();
                break;
        }
    }

    private void Waiting()
    {
        if (controller.State == InputController.Status.Pushed)
        {
            state = Status.Starting;
        }
    }

    private void Starting()
    {
        gameObject.SetActive(false);
    }
}