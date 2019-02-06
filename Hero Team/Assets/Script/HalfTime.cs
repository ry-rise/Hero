using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HalfTime : MonoBehaviour {
    private Text heroPoint;
    private Text Telop;
    private Text powerText;
    private Text lifeText;
    private static int power;
    private static int life;
    private string initTelop = "初期セリフ";
    private string powerTelop = "「勇者パワーを押しました」";
    private string lifeTelop = "「勇者生命力を押しました」";
    private string decideTelop = "「決定を押しました」";

    private void Awake()
    {
        heroPoint = GameObject.Find("HeroPointText").GetComponent<Text>();
        Telop = GameObject.Find("GoddessTelop").GetComponent<Text>();
        powerText = GameObject.Find("HeroPowerText").GetComponent<Text>();
        lifeText = GameObject.Find("HeroLifeText").GetComponent<Text>();
    }
    private void Start()
    {
        //GameManager.HeroPoint = 10;
        heroPoint.text = GameManager.HeroPoint.ToString();
        Telop.text = initTelop;
        powerText.text = power.ToString();
        lifeText.text = life.ToString();
        if (GameManager.SmashLevel == 2)
        {
            GameObject.Find("ButtonPower").GetComponent<Button>().interactable = false;
            GameObject.Find("ButtonLife").GetComponent<Button>().interactable = true;

        }
    }
    private void Update()
    {
        if (GameManager.HeroPoint <= 0)
        {
            GameObject.Find("ButtonPower").GetComponent<Button>().interactable = false;
            GameObject.Find("ButtonLife").GetComponent<Button>().interactable = false;
            GameObject.Find("ButtonNext").GetComponent<Button>().interactable = true;
        }
        //else if(GameManager.SmashLevel>=2)
        //{
        //    GameObject.Find("ButtonPower").GetComponent<Button>().interactable = false;
        //    GameObject.Find("ButtonLife").GetComponent<Button>().interactable = true;
        //}
        else
        {
            GameObject.Find("ButtonPower").GetComponent<Button>().interactable = true;
            GameObject.Find("ButtonLife").GetComponent<Button>().interactable = true;
        }
    }
    public void OnClick(GameObject buttonObject)
    {
        //勇者パワー
        if (buttonObject.name == "ButtonPower")
        {
            power += 1;
            GameManager.HeroPoint -= 1;
            powerText.text = power.ToString();
            heroPoint.text = GameManager.HeroPoint.ToString();
            GameObject.Find("ButtonStop").GetComponent<Button>().interactable = true;
            Telop.text = powerTelop;
            Telop.rectTransform.sizeDelta = new Vector2(Telop.rectTransform.sizeDelta.x, Telop.preferredHeight);
            if (GameObject.Find("ButtonDecide").GetComponent<Button>().interactable == false)
            {
                GameObject.Find("ButtonDecide").GetComponent<Button>().interactable = true;
            }
        }
        //勇者生命力
        if (buttonObject.name == "ButtonLife")
        {
            life += 1;
            GameManager.HeroPoint -= 1;
            lifeText.text = life.ToString();
            heroPoint.text = GameManager.HeroPoint.ToString();
            GameObject.Find("ButtonStop").GetComponent<Button>().interactable = true;
            Telop.text = lifeTelop;
            Telop.rectTransform.sizeDelta = new Vector2(Telop.rectTransform.sizeDelta.x, Telop.preferredHeight);
            if (GameObject.Find("ButtonDecide").GetComponent<Button>().interactable == false)
            {
                GameObject.Find("ButtonDecide").GetComponent<Button>().interactable = true;
            }
        }
        //決定ボタン
        if (buttonObject.name == "ButtonDecide")
        {
            GameObject.Find("ButtonNext").GetComponent<Button>().interactable = true;
            Telop.text = decideTelop;
            Telop.rectTransform.sizeDelta = new Vector2(Telop.rectTransform.sizeDelta.x, Telop.preferredHeight);
            buttonObject.GetComponent<Button>().interactable = false;
        }
        //やめるボタン
        if (buttonObject.name == "ButtonStop")
        {
            GameManager.HeroPoint += (power + life);
            power = 0;
            life = 0;
            powerText.text = power.ToString();
            lifeText.text = life.ToString();
            heroPoint.text = GameManager.HeroPoint.ToString();
            GameObject.Find("ButtonDecide").GetComponent<Button>().interactable = false;
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
            int devidePower = power / 3;
            GameManager.SmashLevel += devidePower;
            GameManager.PlayerLife += life;
            GameManager.HeroPoint -= (power + life);
            SceneManager.LoadScene("MainGame 1");
        }
        //「いいえ」を押すと前の画面に戻る
        if (buttonObject.name == "ButtonNo")
        {
            transform.Find("CheckPanel").gameObject.SetActive(false);
        }
    }
}
