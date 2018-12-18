using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{

    private float timer = 0.0f;
    private float CanvasTimer = 0.0f;
    private EnemyManager enemyManager;
    private GameManager manager;
    [SerializeField]
    GameObject ScrollCanvas;
    //移動距離
    [SerializeField]
    private float moveDistance;
    public float Distance { get { return moveDistance; } }
    //何秒後に着くか
    [SerializeField]
    private float moveEndTime;

    public float DistancePerSecond { get { return moveDistance / moveEndTime; } }
    //背景速度
    [SerializeField]
    private float moveSpeed;

    private Renderer image;

    private Vector2 offset = new Vector2(0, 0);

    [SerializeField]
    Status state = Status.Stopping;
    public Status State { get { return state; } }
    public enum Status
    {
        Stopping,
        Moving,
        Warning
    }
    // Use this for initialization
    public void Scroll()
    {
        float scroll = Mathf.Repeat(moveSpeed / moveEndTime * Time.deltaTime + offset.y, 1);
        offset = new Vector2(0, scroll);
        image.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void Start()
    {
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        image = GetComponent<Renderer>();
        image.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case Status.Stopping:
                Stopping();
                break;
            case Status.Moving:
                Moving();
                break;
            case Status.Warning:
                Warning();
                break;
        }
    }

    private void Stopping()
    {
        if(manager.GameState != GameManager.GameStatus.Wait)
        if (enemyManager.Enemies.Count == 0)
        {
            state = Status.Moving;
        }
    }

    private void Moving()
    {
        //EndTime秒間画面をスクロールする
        timer += Time.deltaTime;
        Scroll();
        if (timer > moveEndTime)
        {
            timer = 0.0f;
            if (enemyManager.LastEnemies())
            {
                Debug.Log("Test");
                ScrollCanvas.SetActive(true);
                state = Status.Warning;
            }
            else
            {
                state = Status.Stopping;
                manager.RequestGameState = GameManager.GameStatus.Wait;
            }
        }
    }

    private void Warning()
    {
        //画面のスクロール処理後にWarning演出をする

        CanvasTimer += Time.deltaTime;
        if (CanvasTimer > 5)
        {
            CanvasTimer = 0.0f;
            ScrollCanvas.SetActive(false);
            state = Status.Stopping;
            manager.RequestGameState = GameManager.GameStatus.Wait;
        }
    }
}