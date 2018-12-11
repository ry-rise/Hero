using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<BaseEnemy> enemies = new List<BaseEnemy>();
    public List<BaseEnemy> Enemies { get { return enemies; } set { enemies = value; } }

    private InputController controller;
    private bool gameStart;

    private void Start()
    {
        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        gameStart = false;
        foreach (BaseEnemy it in Enemies)
        {
            it.stop = BaseEnemy.StopStatus.ALL;
        }
    }

    private void Update()
    {
        if (!gameStart) {
            if (controller.State == InputController.Status.Pushed)
            {
                foreach (BaseEnemy it in Enemies)
                {
                    it.stop = BaseEnemy.StopStatus.None;
                }
                gameStart = true;
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
}