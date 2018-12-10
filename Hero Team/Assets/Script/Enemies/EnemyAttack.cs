using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    [SerializeField]
    private GameObject EnemyBullet;
    [SerializeField]
    EnemyMove enemyMove;
    [SerializeField]
    private float NextTime;
    private float Timer = 0.0f;
    private Vector3 MyPosition;
    public int ListNumber { get; set; }
	void Start () {
        
        enemyMove = GameObject.Find("EnemyManager").GetComponent<EnemyMove>();
        enemyMove.Enemies.Add(this);
        ListNumber = enemyMove.Enemies.Count - 1;
	}
	
	void Update () {
        MyPosition = this.transform.position;
        Timer += Time.deltaTime;
        if (Timer > NextTime) {
            Instantiate(EnemyBullet, MyPosition,new Quaternion(0, 0, 0, 0));
            Timer = 0;
        }
		
	}
}
