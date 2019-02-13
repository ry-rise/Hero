using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goddess : MonoBehaviour
{
    private PlayerManager manager;
    private InputController controller;
    [SerializeField]
    private SpriteRenderer goddessImage;
    [SerializeField]
    private SpriteRenderer backLightImage;
    
    private bool isSwungen = false;

    [SerializeField]
    private ImageStatus[] imageStatuses;
    private Sprite[] goddessImages;

    [System.Serializable]
    public class ImageStatus
    {
        [SerializeField, Range(0f, 1f)]
        private float percent;
        public float Percent { get { return percent; } }
        [SerializeField]
        private Sprite goddessNormal;
        public Sprite GoddessNormal { get { return goddessNormal; } }
        [SerializeField]
        private Sprite goddessSwing;
        public Sprite GoddessSwing { get { return goddessSwing; } }
        [SerializeField]
        private Sprite backLight;
        public Sprite BackLight { get { return backLight; } }
    }

    private Vector2 pastPosition;

    [SerializeField]
    private GameObject fallStopperPrefab;
    private GameObject fallStopper;
    private SpriteRenderer fallStopperSprite;
    private float stopperTimeCount;
    private bool stopperUpper;

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
        ImageChanged();
        IsStoped = false;
        IsSmashStarted = false;
        fallStopper = Instantiate(fallStopperPrefab, new Vector2(0, manager.TapPositionY), Quaternion.identity);
        fallStopperSprite = fallStopper.GetComponent<SpriteRenderer>();
        stopperTimeCount = 0;
        stopperUpper = true;
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
        ImageChanged();
    }

    private void ImageChanged()
    {
        Debug.Log(SmashPercent);
        for (int n = 0; n < imageStatuses.Length; ++n)
        {
            if (imageStatuses[n].Percent <= SmashPercent)
            {
                backLightImage.sprite = imageStatuses[n].BackLight;
                if (isSwungen)
                {
                    goddessImage.sprite = imageStatuses[n].GoddessSwing;
                }
                else
                {
                    goddessImage.sprite = imageStatuses[n].GoddessNormal;
                }
                return;
            }
        }
        //backLight.color = new Color(backLight.color.r, backLight.color.g, backLight.color.b, alpha);
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

    public void SmashAlpha()
    {
        if (stopperUpper)
        {
            stopperTimeCount += Time.deltaTime;
            if (stopperTimeCount >= 1)
            {
                stopperTimeCount = 1;
                stopperUpper = false;
            }
        }
        else
        {
            stopperTimeCount -= Time.deltaTime;
            if (stopperTimeCount <= 0)
            {
                stopperTimeCount = 0;
                stopperUpper = true;
            }
        }
        fallStopperSprite.color = new Color(1, 1, 1, stopperTimeCount);
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
            ImageChanged();
            fallStopper.SetActive(true);
            IsSmashStarted = false;
        }
    }

    public void SmashEnd()
    {
        stopperTimeCount = 0;
        stopperUpper = true;
        fallStopperSprite.color = Color.white;
        fallStopper.SetActive(false);
    }

    public void Swing()
    {
        isSwungen = true;
        ImageChanged();
        StartCoroutine("DelayMethod", 0.3f);
        //Coroutine coroutine = StartCoroutine("DelayMethod", 0.3f);
    }

    private IEnumerator DelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isSwungen = false;
        ImageChanged();
    }
}