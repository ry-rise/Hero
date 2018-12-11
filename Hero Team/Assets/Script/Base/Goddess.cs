using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goddess : MonoBehaviour {

    private GameManager gamemanager;

    private InputController controller;
    public List<Hero> Balls;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private SpriteRenderer backLight;
    [SerializeField]
    private GameObject linePrefab;
    private GameObject line;
    [SerializeField]
    private GameObject tapLine;

    private int layerMask = 1 << 9 | 1 << 13;

    [SerializeField]
    private Edit status;
    public int StartAngle { get { return status.StartAngle; } }
    public int SmashCountMax { get { return status.SmashCount; } }
    private int smashCount;
    public int SmashCount
    {
        get
        {
            return smashCount;
        }
        private set
        {
            smashCount = value;
            if (smashCount > SmashCountMax)
            {
                smashCount = SmashCountMax;
            }
        }
    }

    public float SmashPercent { get { return (float)smashCount / SmashCountMax; } }

    private Vector2 smashVector;

    // Use this for initialization
    private void Awake()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        controller = GameObject.Find("GameManager").GetComponent<InputController>();
        BackLightChanged(SmashPercent);
        smashVector = new Vector2(0, 1);
        line = Instantiate(linePrefab);
    }

    private void Start()
    {
        if (Balls.Count == 0)
        {
            BallSet(true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Moving();
        SmashWaiting();
    }

    private void FixedUpdate()
    {
        if (Balls.Count == 0)
        {
            BallSet();
        }
    }

    private bool TapLineUpperFlag()
    {
        if (controller.TouchPoint.y > tapLine.transform.position.y)
        {
            return true;
        }
        return false;
    }

    private void Moving()
    {
        if (controller.State == InputController.Status.Pressing || controller.State == InputController.Status.Pushed)
        {
            if (TapLineUpperFlag()) return;
            transform.position = new Vector2(controller.TouchMovePoint.x, transform.position.y);
        }
    }

    private void BallSet(bool isStarted = false)
    {
        Instantiate(ballPrefab, status.FirstPosition, Quaternion.identity);
        if (!isStarted)
        {
            //ライフを減らす処理
            gamemanager.LostLife();
        }
    }

    public void SmashCounter(int value)
    {
        SmashCount += value;
        BackLightChanged(SmashPercent);
    }

    private void BackLightChanged(float alpha)
    {
        backLight.color = new Color(backLight.color.r, backLight.color.g, backLight.color.b, alpha);
    }

    private void SmashWaiting()
    {
        if (!TapLineUpperFlag()) return;
        //フリック待ち
        if (controller.State != InputController.Status.Pressing)
        {
            if (line.gameObject.activeInHierarchy)
            {
                line.gameObject.SetActive(false);
            }
            return;
        }

        //ここで線の描画処理を入れる
        LineDraw();
    }

    private void LineDraw()
    {
        if (!line.gameObject.activeInHierarchy)
        {
            line.gameObject.SetActive(true);
        }
        //始点を決める
        Vector2 sPoint = tapLine.transform.position;
        //方向ベクトルを求める
        Vector2 dVector = controller.TouchMovePoint - (Vector2)tapLine.transform.position;
        float scalar = Mathf.Sqrt(dVector.x * dVector.x + dVector.y * dVector.y);
        smashVector = dVector / scalar;
        if (smashVector.y < 0 || scalar == 0) smashVector = new Vector2(0, 1);
        //レイキャスト飛ばします
        RaycastHit2D hit = Physics2D.Raycast(tapLine.transform.position, smashVector, Mathf.Infinity, layerMask);
        //終点を決める
        Vector2 ePoint = hit.point;
        float angle = Mathf.Atan2(smashVector.y, smashVector.x) * Mathf.Rad2Deg - 90;
        line.transform.position = (sPoint + ePoint) / 2;
        line.transform.localScale = new Vector2(0.1f, Vector2.Distance(sPoint, ePoint) * 1.5f);
        line.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 1) * angle);
    }

    public bool Smashing(GameObject ball)
    {
        if (SmashPercent < 1) return false;
        ball.GetComponent<Hero>().TypeChange(true);
        ball.GetComponent<Rigidbody2D>().velocity = smashVector;
        SmashCount = 0;
        BackLightChanged(SmashPercent);
        return true;
    }
}