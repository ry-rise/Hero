using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//コクブ
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private string nextScene;

    public static int PlayerLife = 3; //プレイヤー残機
    public static int SmashLevel = 0;  //スマッシュレベル

    private ItemManager item;   //アイテムマネージャー
    private EnemyManager enemy; //エネミーマネージャー
    private PlayerManager player; //プレイヤーマネージャー
    private BackGroundScroll backGroundScroll;
    private InputController controller; //操作

    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject pauseScreen;

    public GameStatus GameState { get; private set; }   //状態
    public GameStatus RequestGameState { get; set; } //外部から状態を変えたい場合、一度ここを通すこと
    public GameStatus PausePastState { get; private set; }

    //倍数
    private const int barHitCountMultiple = 100;
    private const int getItemCountMultiple = 1200;
    private const int HeroPointCountMultiple = 2000;

    //合計スコア
    private static int totalScore;
    public static int TotalScore { get { return totalScore + EnemyScore + ItemScore + BarScore; } private set { totalScore = value; } }

    //個別スコア
    public static int EnemyScore;   //敵スコア
    public static int BarScore { get { return barHitCountMultiple * BarHitCount; } }     //バー反射スコア
    public static int ItemScore { get { return getItemCountMultiple * GetItemCount; } }    //アイテムゲットスコア
    public static int HeroPointScore { get { return HeroPointCountMultiple * HeroPoint; } } //勇者ポイントスコア

    //スコアに関するカウント
    public static int BarHitCount;  //バーがボールを反射させた回数
    public static int GetItemCount; //アイテムを取得した回数
    public static int HeroPoint; //勇者ポイント

    //計算
    public static void ScoreResult()
    {
        TotalScore += EnemyScore + ItemScore + BarScore;
        EnemyScore = 0;
        BarHitCount = 0;
        GetItemCount = 0;
    }

    public static void ScoreReset()
    {
        TotalScore = 0;
        EnemyScore = 0;
        BarHitCount = 0;
        GetItemCount = 0;
    }

    public enum GameStatus
    {
        Wait,   //ゲーム中（スタート前）
        Play,   //ゲーム中
        Pause,  //一時停止
        NextWave,  //次のウェーブ
        Complete,   //ゲームクリア
        GameOver    //ゲームオーバー
    }

    private void Awake()
    {
        enemy = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        player = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        item = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        backGroundScroll = GameObject.Find("BackGround").GetComponent<BackGroundScroll>();
        controller = GetComponent<InputController>();
        GameState = RequestGameState = GameStatus.Wait;
    }

    private void Update()
    {
        switch (GameState)
        {
            //画面がタップされるまで
            case GameStatus.Wait:
                GameWait();
                break;
            //ゲーム中
            case GameStatus.Play:
                GamePlay();
                break;
            //一時停止
            case GameStatus.Pause:
                GamePause();
                break;
            //敵残数が０になったらクリア
            case GameStatus.NextWave:
                NextWave();
                break;
            case GameStatus.Complete:
                Complete();
                break;
            //プレイヤーの残機が０以下になったらゲームオーバー
            case GameStatus.GameOver:
                GameOver();
                break;
        }
    }

    //プレイヤーのライフが減る処理
    public bool LostLife()
    {
        PlayerLife--;
        if (PlayerLife <= 0)
        {
            RequestGameState = GameStatus.GameOver;
            return true;    //ゲームオーバー
        }
        Debug.Log("Player Life : " + PlayerLife);
        return false;   //ゲーム続行
    }

    //ゲーム開始前
    private void GameWait()
    {
        //画面がタップされたら、スタートする
        if (controller.State == InputController.Status.Pushed)
        {
            enemy.AllStart();   //敵が動き出す
            item.AllStart();    //アイテムが動き出す
            player.BallStart(); //勇者が動き出す
            GameState = RequestGameState = GameStatus.Play;
        }
        else if (RequestGameState == GameStatus.Pause)
        {
            enemy.AllPause();
            item.AllPause();
            player.AllPause();

            pauseScreen.SetActive(true);
            pauseButton.SetActive(false);
            GameState = GameStatus.Pause;
            PausePastState = GameStatus.Wait;
        }
    }

    //ゲーム中
    private void GamePlay()
    {
        //次のウェーブの指示が来たら※敵マネージャーが指示を出す
        if (RequestGameState == GameStatus.NextWave)
        {
            enemy.SetEnemies(new Vector2(0, backGroundScroll.Distance));
            backGroundScroll.ScrollStart();
            player.ResetPosition();
            Debug.Log("WAVE CLEAR!!");
            GameState = GameStatus.NextWave;
        }
        //ゲームクリアの指示が来たら※敵マネージャーが指示を出す
        else if (RequestGameState == GameStatus.Complete)
        {
            Debug.Log("Game CLEAR!!");
            GameState = GameStatus.Complete;
        }
        //ゲームオーバーの指示が来たら※敵マネージャー、もしくは女神が指示を出す
        else if (RequestGameState == GameStatus.GameOver)
        {
            Debug.Log("Game Over!!");
            GameState = GameStatus.GameOver;
        }
        //仕切り直しの指示が来たら※女神が指示を出す
        else if (RequestGameState == GameStatus.Wait)
        {
            enemy.AllStop();    //敵を止める
            item.AllPause();
            GameState = GameStatus.Wait;
        }
        else if (RequestGameState == GameStatus.Pause)
        {
            enemy.AllPause();
            item.AllPause();
            player.AllPause();

            pauseScreen.SetActive(true);
            pauseButton.SetActive(false);
            GameState = GameStatus.Pause;
            PausePastState = GameStatus.Play;
        }
    }

    //一時停止時の挙動
    private void GamePause()
    {
        if (RequestGameState == GameStatus.Play)
        {
            enemy.AllStart();
            item.AllStart();
            player.AllStart();
            pauseScreen.SetActive(false);
            pauseButton.SetActive(true);
            GameState = RequestGameState = PausePastState;
        }
    }

    //Waveクリア時の挙動
    private void NextWave()
    {
        if (backGroundScroll.State == BackGroundScroll.Status.Moving)
        {
            enemy.AllEnemiesMove(new Vector2(0, -backGroundScroll.DistancePerSecond * Time.deltaTime));
        }
        if (RequestGameState == GameStatus.Wait)
        {
            GameState = GameStatus.Wait;
        }
    }

    //ゲームクリア時の挙動
    private void Complete()
    {
        SceneManager.LoadScene(nextScene);
    }

    //ゲームオーバー時の挙動
    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");

    }
}
