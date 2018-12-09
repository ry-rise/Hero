using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour {
    private InputController controller;

    Status state;
    private Text text;
    [SerializeField]
    private string freeText;
    [SerializeField]
    private string waitText;

    enum Status
    {
        Free,
        Waiting,
        Starting
    }

	// Use this for initialization
	void Start ()
    {
        text = GetComponent<Text>();
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        state = Status.Free;
        text.text = freeText;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case Status.Free:
                Free();
                break;
            case Status.Waiting:
                Waiting();
                break;
            case Status.Starting:
                Starting();
                break;
        }
    }

    private void Free()
    {
        if (controller.State == InputController.Status.Pushed || controller.State == InputController.Status.Pressing)
        {
            text.text = waitText;
            state = Status.Waiting;
        }
    }

    private void Waiting()
    {
        if (controller.State == InputController.Status.Released)
        {
            state = Status.Starting;
        }
    }

    private void Starting()
    {
        gameObject.SetActive(false);
    }
}
