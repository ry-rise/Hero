using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {
    [SerializeField]
    private int HP = 1;
    private EnemyMove enemyMove;
    private EnemyAttack enemyAttack;
    private Hero hero;
    bool flag = false;

	// Use this for initialization
	void Start () {
 
       
        enemyAttack = this.gameObject.GetComponent<EnemyAttack>();
	}
	
	// Update is called once per frame
	void Update () {
        if(flag == false)
        {
            enemyMove = GameObject.Find("EnemyManager").GetComponent<EnemyMove>();
            hero = GameObject.Find("Hero(Clone)").GetComponent<Hero>();
            flag = true;
        }
		if(HP < 1)
        {
            
            enemyMove.Enemies.RemoveAt(enemyAttack.ListNumber);
            Destroy(this.gameObject);
           
        }
	}
    void OnTriggerExit2D(Collider2D Target)
    {
        if (Target.gameObject.name == "Hitter")
        {
            HP-= hero.Power;
        }
    }
}
