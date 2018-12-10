using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {
    [SerializeField]
    private int HP = 1;
    private GameObject HeroObject;
    private Hero hero;

	// Use this for initialization
	void Start () {
        HeroObject = GameObject.Find("Hero");
        hero = HeroObject.GetComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
		if(HP < 1)
        {
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
