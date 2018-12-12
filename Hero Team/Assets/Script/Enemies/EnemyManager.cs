using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<BaseEnemy> enemies = new List<BaseEnemy>();
    public List<BaseEnemy> Enemies { get { return enemies; } set { enemies = value; } }

    private InputController controller;
    public bool GameStart { get; private set; }

    private void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        AllStop();
    }

    private void Update()
    {
        if (!GameStart) {
            if (controller.State == InputController.Status.Pushed)
            {
                foreach (BaseEnemy it in Enemies)
                {
                    it.stop = BaseEnemy.StopStatus.None;
                }
                GameStart = true;
            }
        }
    }

    void FixedUpdate()
    {
    }

    public int GetEnemiesCount()
    {
        return Enemies.Count;
    }

    public void AllStop()
    {
        GameStart = false;
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
}