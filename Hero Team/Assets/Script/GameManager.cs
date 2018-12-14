using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コクブ
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool DebugCLEARE = false; //クリア判定のログがうるさいのでこれで切り替える
    [SerializeField] private bool DebugOVER = false;   //ゲームオーバー用
    
    private int PlayerLife = 3; //プレイヤー残機

    private EnemyManager Enemy;
    GameStatus gameState;

    public enum GameStatus
    {
        Play,
        Clear,
        GameOver
    }
    
    private void Awake()
    {
        Enemy = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        gameState = GameStatus.Play;
    }

    private void Update()
    {
        Enemy.GetEnemiesCount();
        //敵残数が０になったらクリア
        if (gameState == GameStatus.Clear)
        {
            GameClear();
        }
        //プレイヤーの残機が０以下になったらゲームオーバー
        else if (gameState == GameStatus.GameOver)
        {
            GameOver();
        }

    }

    //プレイヤーのライフが減る処理
    public void LostLife()
    {
        PlayerLife--;
        if (PlayerLife <= 0)
        {
            GameOverOnFlag();
        }
        Debug.Log("Player Life : " + PlayerLife);
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

    public void GameClearOnFlag()
    {
        gameState = GameStatus.Clear;
    }

    public void GameOverOnFlag()
    {
        gameState = GameStatus.GameOver;
    }
}
