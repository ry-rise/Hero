using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField]
    private int HP = 1;
    private EnemyMove enemyMove;

    // Use this for initialization
    void Start()
    {
        enemyMove = GameObject.Find("EnemyManager").GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP < 1)
        {
            enemyMove.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D target)
    {
        if (target.gameObject.transform.root.tag == "Ball")
        {
            HP -= target.transform.root.GetComponent<Hero>().Power;
        }
    }
}
