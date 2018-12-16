using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コクブ
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool DebugCLEARE = false; //クリア判定のログがうるさいのでこれで切り替える
    [SerializeField] private bool DebugOVER = false;   //ゲームオーバー用
    
    private int PlayerLife = 3; //プレイヤー残機

    private EnemyManager Enemy; //エネミーマネージャー
    private Goddess Player; //プレイヤーマネージャー
    private InputController Controller; //操作

    private GameStatus gameState;   //状態
    public GameStatus RequestGameState { get; set; } //外部から状態を変えたい場合、一度ここを通すこと

    public enum GameStatus
    {
        Wait,   //ゲーム中（スタート前）
        Play,   //ゲーム中
        Pause,  //一時停止
        Clear,  //クリア
        GameOver    //ゲームオーバー
    }
    
    private void Awake()
    {
        Enemy = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        Player = GameObject.Find("Goddess").GetComponent<Goddess>();
        Controller = GetComponent<InputController>();
        gameState = RequestGameState = GameStatus.Wait;
    }

    private void Update()
    {
        switch (gameState)
        {
            //画面がタップされるまで
            case GameStatus.Wait:
                Wait();
                break;
            //ゲーム中
            case GameStatus.Play:
                Play();
                break;
            //一時停止
            case GameStatus.Pause:
                break;
            //敵残数が０になったらクリア
            case GameStatus.Clear:
                GameClear();
                break;
            //プレイヤーの残機が０以下になったらゲームオーバー
            case GameStatus.GameOver:
                GameOver();
                break;
        }
    }

    //プレイヤーのライフが減る処理
    public void LostLife()
    {
        PlayerLife--;
        if (PlayerLife <= 0)
        {
            RequestGameState = GameStatus.GameOver;
        }
        Debug.Log("Player Life : " + PlayerLife);
    }

    //ゲーム開始前
    private void Wait()
    {
        if (Controller.State == InputController.Status.Pushed)
        {
            Enemy.AllStart();
            Player.BallStart();
            gameState = RequestGameState = GameStatus.Play;
        }
    }

    //ゲーム中
    private void Play()
    {
        if (RequestGameState == GameStatus.Clear)
        {
            gameState = GameStatus.Clear;
        }
        else if (RequestGameState == GameStatus.GameOver)
        {
            gameState = GameStatus.GameOver;
        }
        else if (RequestGameState == GameStatus.Wait)
        {
            Enemy.AllStop();
            gameState = GameStatus.Wait;
        }
    }

    //ゲームクリア時の挙動
    private void GameClear()
    {
        if (DebugCLEARE)
        {
            Debug.Log("Game CLEAR!!");
        }
    }

    //ゲームオーバー時の挙動
    private void GameOver()
    {
        if (DebugOVER)
        {
            Debug.Log("Game Over!!");
        }
    }
}
