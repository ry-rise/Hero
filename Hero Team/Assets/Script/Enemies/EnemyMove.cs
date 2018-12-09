using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {
    public List<EnemyAttack> Enemies;

    void FixedUpdate()
    {
        for (int i = 0; i <= Enemies.Count - 1; i += 1)
        {
            Enemies[i].gameObject.transform.Translate(0, -0.01f, 0);
        }
    }
}
