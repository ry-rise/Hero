using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> Enemies { get { return enemies; } set { enemies = value; } }

    void FixedUpdate()
    {
        for (int i = 0; i <= Enemies.Count - 1; i += 1)
        {
            Enemies[i].transform.Translate(0, -0.01f, 0);
        }
    }

    public int GetEnemiesCount()
    {
        return Enemies.Count;
    }
}