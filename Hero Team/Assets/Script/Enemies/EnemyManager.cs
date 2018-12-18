using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private EnemiesDataTable enemiesList;   //呼び出す敵たち
    private List<BaseEnemy> enemies = new List<BaseEnemy>();
    public List<BaseEnemy> Enemies { get { return enemies; } set { enemies = value; } }

    private WallHitter wallHitter;
    private GameManager gameManager;

    private void Start()
    {
        wallHitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (enemiesList != null) {
            foreach (EnemiesSetStatus it in enemiesList.Status)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Enemies/" + it.EnemyName + ".prefab");
                GameObject enemy = Instantiate(prefab, it.Position, Quaternion.identity);
            }
        }
        AllStop();
    }

    private void Update()
    {
    }

    void FixedUpdate()
    {
        GameInChecker();
        LineOutChecker();
        GameClearChecker();
    }

    public void AllStop()
    {
        foreach (BaseEnemy it in Enemies)
        {
            it.stop = BaseEnemy.StopStatus.ALL;
        }
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject it in gameObjects)
        {
            Destroy(it);
        }
    }

    public void AllStart()
    {
        foreach (BaseEnemy it in Enemies)
        {
            if (wallHitter.IsHit(it.gameObject, HitPointFlag.GameIn))
            {
                it.stop = BaseEnemy.StopStatus.None;
            }
            else
            {
                it.stop = BaseEnemy.StopStatus.AttackStoped;
            }
        }
    }

    private void GameClearChecker()
    {
        if (gameManager.GameState != GameManager.GameStatus.Play) return;
        if (Enemies.Count == 0)
        {
            gameManager.RequestGameState = GameManager.GameStatus.Clear;
        }
    }

    private void GameInChecker()
    {
        foreach (BaseEnemy it in Enemies)
        {
            if (it.stop != BaseEnemy.StopStatus.AttackStoped) continue;
            if (wallHitter.IsHit(it.gameObject, HitPointFlag.GameIn))
            {
                it.stop = BaseEnemy.StopStatus.None;
            }
        }
    }

    private void LineOutChecker()
    {
        if (gameManager.GameState != GameManager.GameStatus.Play) return;
        foreach (BaseEnemy it in Enemies)
        {
            if (it.stop == BaseEnemy.StopStatus.MoveStoped || it.stop == BaseEnemy.StopStatus.ALL) continue;
            if (wallHitter.IsHit(it.gameObject, HitPointFlag.Bottom))
            {
                gameManager.RequestGameState = GameManager.GameStatus.GameOver;
                break;
            }
        }
    }
}