using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HalfTime : MonoBehaviour {
    private static int life;
    private Text HeroPointText;
    private void Awake()
    {
        HeroPointText = GameObject.Find("HeroPointText").GetComponent<Text>();
        HeroPointText.text = GameManager.HeroPoint.ToString();
    }
    public void OnClick(GameObject buttonObject)
    {
        //「元に戻す」ボタン
        if (buttonObject.name == "ButtonRestore")
        {
            GameManager.HeroPoint += life;
            life = 0;
            buttonObject.GetComponent<Button>().interactable = false;
        }
        //「つぎへ」を押すと確認画面に
        if (buttonObject.name == "ButtonNext")
        {
            transform.Find("CheckPanel").gameObject.SetActive(true);
        }
        //「はい」を押すとステージに移動
        if (buttonObject.name == "ButtonYes")
        {
            GameManager.PlayerLife += life;
            GameManager.HeroPoint -=  life;
            SceneManager.LoadScene("MainGame 1");
        }
        //「いいえ」を押すと前の画面に戻る
        if (buttonObject.name == "ButtonNo")
        {
            transform.Find("CheckPanel").gameObject.SetActive(false);
        }
    }
}
