using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    private InputController controller;

    Status state;
    private Text text;
    [SerializeField]
    private string waitText;

    enum Status
    {
        Waiting,
        Starting
    }

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        state = Status.Waiting;
        text.text = waitText;
    }

    // Update is called once per frame
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
            text.text = waitText;
            state = Status.Starting;
        }
    }

    private void Starting()
    {
        gameObject.SetActive(false);
    }
}