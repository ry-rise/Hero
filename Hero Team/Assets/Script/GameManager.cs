using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//コクブ
public class GameManager : MonoBehaviour
{
    private bool FlagCLEARE = true; 
    private bool FlagOVER =   true;   
    
    private int PlayerLife = 3; //プレイヤー残機

    private EnemyManager Enemy; //エネミーマネージャー
    private Goddess Player; //プレイヤーマネージャー
    private BackGroundScroll _BackGroundScroll;
    private InputController Controller; //操作

    public GameStatus GameState { get; private set; }   //状態
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
        _BackGroundScroll = GameObject.Find("BackGround").GetComponent<BackGroundScroll>();
        Controller = GetComponent<InputController>();
        GameState = RequestGameState = GameStatus.Wait;
    }

    private void Update()
    {
        switch (GameState)
        {
            //画面がタップされるまで
            case GameStatus.Wait:
                GameWait();
                break;
            //ゲーム中
            case GameStatus.Play:
                GamePlay();
                break;
            //一時停止
            case GameStatus.Pause:
                GamePause();
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
    public bool LostLife()
    {
        PlayerLife--;
        if (PlayerLife <= 0)
        {
            RequestGameState = GameStatus.GameOver;
            return true;    //ゲームオーバー
        }
        Debug.Log("Player Life : " + PlayerLife);
        return false;   //ゲーム続行
    }

    //ゲーム開始前
    private void GameWait()
    {
        //画面がタップされたら、スタートする
        if (Controller.State == InputController.Status.Pushed)
        {
            Enemy.AllStart();   //敵が動き出す
            Player.BallStart(); //勇者が動き出す
            GameState = RequestGameState = GameStatus.Play;
        }
    }

    //ゲーム中
    private void GamePlay()
    {   
        //ゲームクリアの指示が来たら※敵マネージャーが指示を出す
        if (RequestGameState == GameStatus.Clear)
        {
            GameState = GameStatus.Clear;
        }
        //ゲームオーバーの指示が来たら※敵マネージャー、もしくは女神が指示を出す
        else if (RequestGameState == GameStatus.GameOver)
        {
            GameState = GameStatus.GameOver;
        }
        //仕切り直しの指示が来たら※女神が指示を出す
        else if (RequestGameState == GameStatus.Wait)
        {
            Enemy.AllStop();    //敵を止める
            GameState = GameStatus.Wait;
        }
    }

    //一時停止時の挙動
    private void GamePause()
    {

    }

    //ゲームクリア時の挙動
    private void GameClear()
    {
        if (FlagCLEARE)
        {
            _BackGroundScroll.ScrollFlag = true;
            Debug.Log("Game CLEAR!!");
            FlagCLEARE = false;
        }
    }

    //ゲームオーバー時の挙動
    private void GameOver()
    {
        if (FlagOVER)
        {
            Debug.Log("Game Over!!");
            FlagOVER = false;
        }
    }
}
