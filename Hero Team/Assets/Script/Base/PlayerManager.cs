using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField]
    private SeManager se;
    [SerializeField]
    private GameObject ballPrefab;
    private List<Hero> balls = new List<Hero>();
    public List<Hero> Balls { get { return balls; } set { balls = value; } }
    [SerializeField]
    private GameObject tapLinePrefab;
    [SerializeField]
    private float tapPositionY;
    public float TapPositionY { get { return tapPositionY; } }
    private Goddess goddess;
    private Bar bar;

    public float StartAngle { get { return goddess.StartAngle; } }

    public Vector2 FirstPosition { get { return goddess.FirstPosition; } }

    public bool IsPenetrated { get; private set; }
    private float penetratTimeCount;
    [SerializeField]
    private List<StatusEdit> statuses;
    public List<StatusEdit> Statuses { get { return statuses; } }

    private void Awake()
    {
        Instantiate(tapLinePrefab, new Vector2(0, TapPositionY), Quaternion.identity);
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        goddess = GameObject.FindGameObjectWithTag("Player").GetComponent<Goddess>();
        bar = goddess.transform.Find("Bar").GetComponent<Bar>();
        penetratTimeCount = 0;
        IsPenetrated = false;
        if (Balls.Count == 0)
        {
            BallSet(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Balls.Count == 0)
        {
            BallSet();
        }
        if (IsPenetrated)
        {
            SmashTime();
        }
    }
    public void SmashCounter(int value)
    {
        goddess.SmashCounter(value);
    }

    private void SmashTime()
    {
        if (goddess.IsStoped) return;
        if (IsPenetrated)
        {
            if (penetratTimeCount < statuses[GameManager.SmashLevel].LimitTime)
            {
                penetratTimeCount += Time.fixedDeltaTime;
            }
            else
            {
                SmashReset();
            }
        }
    }

    private void SmashReset()
    {
        IsPenetrated = false;
        penetratTimeCount = 0;
        goddess.SmashEnd();
    }

    private void BallSet(bool isStarted = false)
    {
        if (!isStarted)
        {
            if (gameManager.GameState == GameManager.GameStatus.GameOver || gameManager.RequestGameState == GameManager.GameStatus.GameOver) return;
            se.Play();
            bool flag = gameManager.LostLife(); //ライフを減らす処理
            if (flag) return;
            gameManager.RequestGameState = GameManager.GameStatus.Wait;
            bar.ResetScale();       //バーのサイズをリセット
            goddess.transform.position = new Vector2(0, goddess.transform.position.y);
            goddess.IsStoped = true;
        }
        Instantiate(ballPrefab, goddess.FirstPosition, Quaternion.identity);
    }

    public void BallStart()
    {
        if (Balls.Count == 0) return;
        foreach (Hero it in Balls)
        {
            it.StartGame();
        }
        goddess.IsStoped = false;
    }

    public void ResetPosition()
    {
        if (Balls.Count == 0) return;
        foreach (Hero it in Balls)
        {
            it.ResetPosition();
        }
        goddess.transform.position = new Vector2(0, goddess.transform.position.y);
        SmashReset();
        goddess.IsStoped = true;
    }

    public void AllPause()
    {
        foreach (Hero it in Balls)
        {
            it.IsStoped = true;
        }
        goddess.IsStoped = true;
    }

    public void AllStart()
    {
        foreach (Hero it in Balls)
        {
            it.IsStoped = false;
        }
        goddess.IsStoped = false;
    }

    public void AllSmashing(Vector2 point)
    {
        foreach (Hero it in balls)
        {
            //方向ベクトルを求める
            Vector2 dVector = point - (Vector2)it.transform.position;
            float scalar = Mathf.Sqrt(dVector.x * dVector.x + dVector.y * dVector.y);
            Vector2 smashVector = dVector / scalar;
            it.GetComponent<Rigidbody2D>().velocity = smashVector * it.Speed;
        }
        IsPenetrated = true;
    }
}