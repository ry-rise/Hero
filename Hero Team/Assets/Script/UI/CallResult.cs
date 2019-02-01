using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallResult : MonoBehaviour {
    [SerializeField]
    private Text HeroPointText;
    [SerializeField]
    private Text EnemyText;
    [SerializeField]
    private Text RefectionText;
    [SerializeField]
    private Text ItemText;
    [SerializeField]
    private Text TotalText;
	// Use this for initialization
	void Start () {
        HeroPointText.text = "勇者ポイント" + GameManager.HeroPointScore;
        EnemyText.text = "倒した敵   " + GameManager.EnemyScore;
        RefectionText.text = "ぶっとばし   " + GameManager.BarScore;
        ItemText.text = "アイテム   " + GameManager.ItemScore;
        TotalText.text = "合計スコア   " + GameManager.TotalScore;
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
