using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	void Start () {
        GameObject.Find("EnemyManager").GetComponent<EnemyMove>().Enemies.Add(this);
	}
	
	void Update () {
		
	}
}
