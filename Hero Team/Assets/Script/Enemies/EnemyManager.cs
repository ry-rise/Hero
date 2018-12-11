using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<BaseEnemy> enemies = new List<BaseEnemy>();
    public List<BaseEnemy> Enemies { get { return enemies; } set { enemies = value; } }

    void FixedUpdate()
    {
        for (int i = 0; i <= Enemies.Count - 1; ++i)
        {
            Enemies[i].MoveSwitch();    //移動制御
            Enemies[i].AttackSwitch();  //攻撃制御
        }
    }

    public int GetEnemiesCount()
    {
        return Enemies.Count;
    }
}