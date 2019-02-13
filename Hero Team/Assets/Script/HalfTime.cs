using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HalfTime : MonoBehaviour {
    private int life;
    private Text HeroPointText;
    private Text TelopText;
    private string TelopInit = "次の戦いに備えましょう";
    private string TelopRecovery = "HPは回復したわ。\nこれでまた思いっきりぶっとばせるわね。";
    private string TelopNext = "さあ、先に進むわよ！";
    private GameObject[] hearts;
    private GameObject buttonRecovery;
    private GameObject goddessImage;
    [SerializeField] private Sprite heartDark;
    [SerializeField] private Sprite heartLight;
    [SerializeField] private Sprite recoveryOn;
    [SerializeField] private Sprite recoveryOff;

    private void Awake()
    {
        //ハート、HP回復、女神を取得
        hearts = new GameObject[3];
        hearts[0] = GameObject.Find("Heart1");
        hearts[1] = GameObject.Find("Heart2");
        hearts[2] = GameObject.Find("Heart3");
        buttonRecovery = GameObject.Find("ButtonRecovery");
        goddessImage = GameObject.Find("GoddessImage");
        //デバッグ
        GameManager.PlayerLife = 1;
        GameManager.HeroPoint = 10;
        //勇者ポイント、テロップのTextを取得
        HeroPointText = GameObject.Find("HeroPointText").GetComponent<Text>();
        HeroPointText.text = GameManager.HeroPoint.ToString();
        TelopText = GameObject.Find("TelopText").GetComponent<Text>();
        TelopText.text = TelopInit;
        HeartSpriteChange();
    }
    private void Update()
    {
        //勇者ポイントが0になると「HP回復」ボタンが押せなくなる
        if (GameManager.HeroPoint <= 0)
        {
            buttonRecovery.GetComponent<Image>().sprite = recoveryOff;
            buttonRecovery.GetComponent<Button>().interactable = false;
        }
    }
    /// <summary>
    /// プレイヤー残機の数によってハートの画像を差し替え
    /// </summary>
    private void HeartSpriteChange()
    {
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
                buttonRecovery.GetComponent<Image>().sprite = recoveryOff;
                buttonRecovery.GetComponent<Button>().interactable = false;
                break;
        }
    }
    public void OnClick(GameObject buttonObject)
    {
        //「HP回復」ボタン
        if (buttonObject.name == "ButtonRecovery")
        {
            GameManager.HeroPoint -= 1;
            GameManager.PlayerLife += 1;
            HeroPointText.text = GameManager.HeroPoint.ToString();
            life += 1;
            HeartSpriteChange();
            GameObject.Find("ButtonRestore").GetComponent<Button>().interactable = true;
            TelopText.text = TelopRecovery;
            TelopText.fontSize = 160;
        }
        //「元に戻す」ボタン
        if (buttonObject.name == "ButtonRestore")
        {
            GameManager.HeroPoint += life;
            GameManager.PlayerLife -= life;
            HeroPointText.text = GameManager.HeroPoint.ToString();
            life = 0;
            HeartSpriteChange();
            buttonRecovery.GetComponent<Image>().sprite = recoveryOn;
            buttonRecovery.GetComponent<Button>().interactable = true;
            buttonObject.GetComponent<Button>().interactable = false;
            TelopText.text = TelopInit;
            TelopText.fontSize = 200;
        }
        //「つぎへ」を押すと確認画面に
        if (buttonObject.name == "ButtonNext")
        {
            TelopText.text = TelopNext;
            Transform checkPanel = transform.Find("CheckPanel");
            checkPanel.gameObject.SetActive(true);
            goddessImage.transform.SetParent(checkPanel);
        }
        //「はい」を押すとステージに移動
        if (buttonObject.name == "ButtonYes")
        {
            switch (GameManager.StageNumber)
            {
                case 1:
                    SceneManager.LoadScene("Stage2");
                    GameManager.StageNumber += 1;
                    break;
                case 2:
                    SceneManager.LoadScene("Stage3");
                    GameManager.StageNumber += 1;
                    break;
                case 3:
                    SceneManager.LoadScene("Stage4");
                    break;
            }
        }
        //「いいえ」を押すと前の画面に戻る
        if (buttonObject.name == "ButtonNo")
        {
            Transform checkPanel = transform.Find("CheckPanel");
            checkPanel.gameObject.SetActive(false);
            goddessImage.transform.SetParent(transform);
            TelopText.text = TelopInit;
            TelopText.fontSize = 200;
        }
    }
}
