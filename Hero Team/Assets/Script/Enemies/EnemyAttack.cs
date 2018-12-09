using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    private GameObject EnemyBullet;
    [SerializeField]
    private float NextTime;
    private float Timer = 0.0f;
    private Vector3 MyPosition;
	void Start () {
        
        GameObject.Find("EnemyManager").GetComponent<EnemyMove>().Enemies.Add(this);
        MyPosition = this.transform.position;
	}
	
	void Update () {
        Timer += Time.deltaTime;
        if (Timer > NextTime) {
            Instantiate(EnemyBullet, MyPosition,new Quaternion(0, 0, 0, 0));
            Timer = 0;
        }
		
	}
}
