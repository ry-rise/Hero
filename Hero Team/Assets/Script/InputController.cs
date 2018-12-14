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
        PressingMove,
        Released,
    }

    private GameObject backGoround;

    [SerializeField]
    private bool mouseMode;

    // Use this for initialization
    void Start()
    {
        State = Status.Free;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        backGoround = GameObject.Find("BackGround");
    }

    // Update is called once per frame
    void Update()
    {
        if (mouseMode)
        {
            //タップに関する何か
            if (Input.GetMouseButtonDown(0))
            {
                if (!BackGroundIsTaped(backGoround)) return;
                State = Status.Pushed;
                TouchMovePoint = TouchPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                State = Status.Released;
            }
            else if (Input.GetMouseButton(0))
            {
                if (TouchMovePoint != (Vector2)Input.mousePosition)
                {
                    if (!BackGroundIsTaped(backGoround)) return;
                    State = Status.PressingMove;
                    TouchMovePoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    State = Status.Pressing;
                }
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
                    if (!BackGroundIsTaped(backGoround)) return;
                    State = Status.Pushed;
                    TouchMovePoint = TouchPoint = mainCamera.ScreenToWorldPoint(Input.touches[0].position);
                }
                else if (Input.touches[0].phase == TouchPhase.Moved)
                {
                    if (!BackGroundIsTaped(backGoround)) return;
                    State = Status.PressingMove;
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

    private bool BackGroundIsTaped(GameObject myObject)
    {
        int srcId = myObject.GetInstanceID();
        Vector3 pos;
        if (mouseMode)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        }
        Collider2D[] colliders = Physics2D.OverlapPointAll(pos);
        foreach(Collider2D collider in colliders)
        {
            int dstId = collider.gameObject.GetInstanceID();
            if (srcId == dstId)
            {
                return true;
            }
        }
        return false;
    }
}