using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goddess : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer backLight;
    private PlayerManager manager;
    private InputController controller;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private Sprite[] GoddessSprite;

    private Vector2 pastPosition;

    [SerializeField]
    private GameObject fallStopperPrefab;
    private GameObject fallStopper;

    [SerializeField]
    private Edit status;
    public Vector2 FirstPosition { get { return status.FirstPosition; } }
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

    public bool IsStoped { get; set; }

    private bool IsSmashStarted { get; set; }

    // Use this for initialization
    private void Awake()
    {
        manager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        BackLightChanged(SmashPercent);
        IsStoped = false;
        IsSmashStarted = false;
        fallStopper = Instantiate(fallStopperPrefab, new Vector2(0, manager.TapPositionY), Quaternion.identity);
    }

    // Update is called once per frame
    private void Update()
    {
        Moving();
        SmashWaiting();
    }

    public enum ControlStatus
    {
        None,
        Bar,
        Smash,
    }

    private ControlStatus OnController()
    {
        //指の初期位置
        if (controller.State == InputController.Status.Pushed &&
            controller.TouchPoint.y < manager.TapPositionY)
        {
            pastPosition = transform.position;
            return ControlStatus.None;
        }
        //バーの操作
        if ((controller.State == InputController.Status.Pressing ||
            controller.State == InputController.Status.PressingMove) &&
            controller.TouchPoint.y < manager.TapPositionY)
        {
            return ControlStatus.Bar;
        }
        //スマッシュ
        if (controller.State == InputController.Status.Pushed && 
            controller.TouchPoint.y > manager.TapPositionY)
        {
            return ControlStatus.Smash;
        }
        return ControlStatus.None;
    }

    private void Moving()
    {
        if (OnController() != ControlStatus.Bar || IsStoped)
        {
            return;
        }
        transform.position = pastPosition + new Vector2(controller.TouchMovePoint.x - controller.TouchPoint.x, 0);
    }

    public void SmashCounter(int value)
    {
        if (manager.IsPenetrated) return;
        SmashCount += value;
        BackLightChanged(SmashPercent);
    }

    private void BackLightChanged(float alpha)
    {
        backLight.color = new Color(backLight.color.r, backLight.color.g, backLight.color.b, alpha);
    }

    private void SmashWaiting()
    {
        if (SmashPercent < 1 || IsStoped)
        {
            IsSmashStarted = false;
            return;
        }
        Smashing();
    }

    public void Smashing()
    {
        if (!IsSmashStarted)
        {
            IsSmashStarted = true;
            return;
        }
        if (OnController() == ControlStatus.Smash)
        {
            manager.AllSmashing(controller.TouchPoint);
            SmashCount = 0;
            BackLightChanged(SmashPercent);
            fallStopper.SetActive(true);
            IsSmashStarted = false;
        }
    }

    public void SmashEnd()
    {
        fallStopper.SetActive(false);
    }

    public void Swing()
    {
        sprite.sprite = GoddessSprite[1];
        StartCoroutine("DelayMethod", 0.3f);
        //Coroutine coroutine = StartCoroutine("DelayMethod", 0.3f);
    }

    private IEnumerator DelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        sprite.sprite = GoddessSprite[0];
    }
}