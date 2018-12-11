using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コクブ
public class GameManager : MonoBehaviour
{
    [SerializeField] private bool DebugCLEARE = false; //クリア判定のログがうるさいのでこれで切り替える
    
    private int EnemyCount = 0; //敵残数
    private int PlayerLife = 3; //プレイヤー残機

    private EnemyManager Enemy;
    
    private void Awake()
    {
        Enemy = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
    }

    private void Update()
    {
        EnemyCount = Enemy.GetEnemiesCount();
        //敵残数が０になったらクリア
        if (EnemyCount <= 0)
        {
            GameClear();
        }
        //プレイヤーの残機が０以下になったらゲームオーバー
        if (PlayerLife < 0)
        {
            GameOver();
        }

    }

    //プレイヤーのライフが減る処理
    public void LostLife()
    {
        PlayerLife--;
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
        Debug.Log("Game Over!!");
    }
}
