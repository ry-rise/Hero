using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HalfTime : MonoBehaviour {
    private static int life;
    private Text HeroPointText;
    private string TelopInit = "次の戦いに備えましょう";
    private string TelopRecovery = "HPは回復したわ。これでまた思いっきりぶっとばせるわね。";
    private string TelopNext = "さあ、先に進むわよ！";
    private GameObject[] hearts;
    [SerializeField] private Sprite heartDark;
    [SerializeField] private Sprite heartLight;

    private void Awake()
    {
        hearts = new GameObject[3];
        hearts[0] = GameObject.Find("Heart1");
        hearts[1] = GameObject.Find("Heart2");
        hearts[2] = GameObject.Find("Heart3");
        GameManager.PlayerLife = 1;
        switch (GameManager.PlayerLife)
        {
            case 1:
                hearts[0].GetComponent<Image>().sprite = heartLight;
                hearts[1].GetComponent<Image>().sprite = heartDark;
                hearts[2].GetComponent<Image>().sprite = heartDark;
                break;
            case 2:
                hearts[0].GetComponent<Image>().sprite = heartLight;
                hearts[1].GetComponent<Image>().sprite = heartLight;
                hearts[2].GetComponent<Image>().sprite = heartDark;
                break;
            case 3:
                hearts[0].GetComponent<Image>().sprite = heartLight;
                hearts[1].GetComponent<Image>().sprite = heartLight;
                hearts[2].GetComponent<Image>().sprite = heartLight;
                break;
        }
        HeroPointText = GameObject.Find("HeroPointText").GetComponent<Text>();
        HeroPointText.text = GameManager.HeroPoint.ToString();
    }
    private void Update()
    {
        if (GameManager.HeroPoint <= 0)
        {
            GameObject.Find("ButtonRecovery").GetComponent<Button>().interactable = false;
        }
    }
    public void OnClick(GameObject buttonObject)
    {
        //「HP回復」ボタン
        if (buttonObject.name == "ButtonRecovery")
        {
            GameManager.HeroPoint -= 1;
            life += 1;
            GameObject.Find("ButtonRestore").GetComponent<Button>().interactable = true;

        }
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
            SceneManager.LoadScene("");
        }
        //「いいえ」を押すと前の画面に戻る
        if (buttonObject.name == "ButtonNo")
        {
            transform.Find("CheckPanel").gameObject.SetActive(false);
        }
    }
}
