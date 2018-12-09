using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goddess : MonoBehaviour {

    private InputController controller;
    public List<Hero> Balls;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private SpriteRenderer backLight;

    [SerializeField]
    private Edit status;
    public int StartAngle { get { return status.StartAngle; } }
    public int SmashCountMax { get { return status.SmashCount; } }
    private int smashCount;
    public int SmashCount
    {
        get
        {
            return smashCount;
        }
        private set
        {
            smashCount = value;
            if (smashCount > SmashCountMax)
            {
                smashCount = SmashCountMax;
            }
        }
    }

    public float SmashPercent { get { return (float)smashCount / SmashCountMax; } }

    // Use this for initialization
    private void Awake()
    {
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        BackLightChanged(SmashPercent);
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

    private void BallSet(bool isStarted = false)
    {
        Instantiate(ballPrefab, status.FirstPosition, Quaternion.identity);
        if (!isStarted)
        {
            //ライフを減らす処理
        }
    }

    public void SmashCounter(int value)
    {
        SmashCount += value;
        BackLightChanged(SmashPercent);
    }

    public void BackLightChanged(float alpha)
    {
        backLight.color = new Color(backLight.color.r, backLight.color.g, backLight.color.b, alpha);
    }
}