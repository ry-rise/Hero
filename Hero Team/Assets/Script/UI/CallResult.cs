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
    [SerializeField]
    private Text GoddessText;
    public int[] StandardScore;
    // Use this for initialization
    void Start()
    {
        HeroPointText.text = "勇者ポイント  " + GameManager.HeroPointScore;
        EnemyText.text = "倒した敵   " + GameManager.EnemyScore;
        RefectionText.text = "ぶっとばし   " + GameManager.BarScore;
        ItemText.text = "アイテム   " + GameManager.ItemScore;
        TotalText.text = "合計スコア   " + GameManager.TotalScore;
        if (GameManager.TotalScore >= StandardScore[2] * (1 << GameManager.SelectLevel))
        {
            GoddessText.text = "･･･凄まじい活躍ね。もはや神の領域だわ。";
        }
        else if (GameManager.TotalScore >= StandardScore[1] * (1 << GameManager.SelectLevel))
        {
            GoddessText.text = "まさに勇者に相応しい活躍ね。お見事！";
        }
        else if (GameManager.TotalScore >= StandardScore[0] * (1 << GameManager.SelectLevel))
        {
            GoddessText.text = "ひとまず世界は救われたわね。おつかれさま。";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
