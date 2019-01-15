using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
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


    private void Awake()
    {
        Instantiate(tapLinePrefab, new Vector2(0, TapPositionY), Quaternion.identity);
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        goddess = GameObject.FindGameObjectWithTag("Player").GetComponent<Goddess>();
        bar = goddess.transform.Find("Bar").GetComponent<Bar>();
        if (Balls.Count == 0)
        {
            BallSet(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //アイテムの使用
    }

    private void FixedUpdate()
    {
        if (Balls.Count == 0)
        {
            BallSet();
        }
    }

    private void BallSet(bool isStarted = false)
    {
        if (!isStarted)
        {
            if (gameManager.GameState == GameManager.GameStatus.GameOver || gameManager.RequestGameState == GameManager.GameStatus.GameOver) return;
            bool flag = gameManager.LostLife(); //ライフを減らす処理
            if (flag) return;
            gameManager.RequestGameState = GameManager.GameStatus.Wait;
            bar.ResetScale();       //バーのサイズをリセット
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
    }

    public void ResetPosition()
    {
        if (Balls.Count == 0) return;
        foreach (Hero it in Balls)
        {
            it.ResetPosition();
        }
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
}