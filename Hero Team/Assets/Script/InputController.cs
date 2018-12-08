using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //カメラ
    private Camera mainCamera;
    //タッチした時の座標
    public Vector2 TouchPoint { get; private set; }
    //タッチしている時の座標
    public Vector2 TouchMovePoint { get; private set; }
    //画面タッチの状態
    public Status State { get; private set; }
    public enum Status
    {
        Free,
        Pushed,
        Pressing,
        Released
    }

    [SerializeField]
    private bool mouseMode;

    // Use this for initialization
    void Start()
    {
        State = Status.Free;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseMode)
        {
            //タップに関する何か
            if (Input.GetMouseButtonDown(0))
            {
                State = Status.Pushed;
                TouchMovePoint = TouchPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                State = Status.Released;
            }
            else if (Input.GetMouseButton(0))
            {
                State = Status.Pressing;
                TouchMovePoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                State = Status.Free;
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                //タップに関する何か
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    State = Status.Pushed;
                    TouchMovePoint = TouchPoint = mainCamera.ScreenToWorldPoint(Input.touches[0].position);
                }
                else if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    State = Status.Pressing;
                    TouchMovePoint = mainCamera.ScreenToWorldPoint(Input.touches[0].position);
                }
                else if (Input.touches[0].phase == TouchPhase.Stationary)
                {
                    State = Status.Pressing;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    State = Status.Released;
                }
            }
            else
            {
                State = Status.Free;
            }
        }
    }
}