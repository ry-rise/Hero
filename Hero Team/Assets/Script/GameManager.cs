using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//コクブ
public class GameManager : MonoBehaviour
{
    private int EnemyCount = 0; //敵残数
    private int PlayerLife = 3; //プレイヤー残機

    private EnemyMove Enemy;


    private void Start()
    {
        Enemy = GameObject.Find("EnemyManager").GetComponent<EnemyMove>();
    }

    private void Update()
    {
        EnemyCount = Enemy.GetEnemiesCount();
        //敵残数が０になったらクリア
        if (EnemyCount <= 0)
        {
            GameClear();
        }
    }

    //ゲームクリア時の挙動
    void GameClear()
    {
        Debug.Log("Game CLEAR!!");
    }

    //ゲームオーバー時の挙動
    void GameOver()
    {
        Debug.Log("Game Over!!");
    }
}
