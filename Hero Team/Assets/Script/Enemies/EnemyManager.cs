using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemiesDataTable> enemiesList;   //呼び出す敵たち
    private List<BaseEnemy> enemies = new List<BaseEnemy>();
    public List<BaseEnemy> Enemies { get { return enemies; } set { enemies = value; } }
    private List<EnemyBullet> bullets = new List<EnemyBullet>();
    public List<EnemyBullet> Bullets { get { return bullets; } set { bullets = value; } }

    private WallHitter wallHitter;
    private GameManager gameManager;
    private int waveNumber;

    private void Start()
    {
        wallHitter = GameObject.Find("GameManager").GetComponent<WallHitter>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetEnemies(Vector2.zero);
        waveNumber = 0;
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

    public bool LastEnemies()
    {
        if (enemiesList != null && waveNumber == enemiesList.Count - 1)
        {
            return true;
        }
        return false;
    }

    public void SetEnemies(Vector2 addPosition)
    {
        if (enemiesList != null && waveNumber < enemiesList.Count)
        {
            foreach (EnemiesSetStatus it in enemiesList[waveNumber].Status)
            {
                GameObject prefab = Resources.Load("Enemies/" + it.EnemyName) as GameObject;
                Instantiate(prefab, it.Position + addPosition, Quaternion.identity);
            }
            AllStop();
        }
    }

    public void AllEnemiesMove(Vector2 move)
    {
        foreach (BaseEnemy it in enemies)
        {
            it.transform.Translate(move);
        }
    }

    public void AllPause()
    {
        foreach (BaseEnemy it in Enemies)
        {
            it.stop = BaseEnemy.StopStatus.ALL;
        }
        foreach (EnemyBullet it in Bullets)
        {
            it.IsStoped = true;
        }
    }

    public void AllStop()
    {
        foreach (BaseEnemy it in Enemies)
        {
            it.stop = BaseEnemy.StopStatus.ALL;
        }
        foreach (EnemyBullet it in Bullets)
        {
            Destroy(it.gameObject);
        }
        Bullets.Clear();
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
        foreach (EnemyBullet it in Bullets)
        {
            it.IsStoped = false;
        }
    }

    private void GameClearChecker()
    {
        if (gameManager.GameState != GameManager.GameStatus.Play) return;
        if (Enemies.Count == 0)
        {
            ++waveNumber;
            if (waveNumber < enemiesList.Count)
            {
                gameManager.RequestGameState = GameManager.GameStatus.NextWave;
            }
            else
            {
                gameManager.RequestGameState = GameManager.GameStatus.Complete;
            }
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