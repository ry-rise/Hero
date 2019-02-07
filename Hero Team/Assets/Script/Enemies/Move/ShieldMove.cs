using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMove : BaseEnemyMove
{
    [SerializeField]
    private GameObject target;
    public Vector2 CenterPoint { get { return target.transform.position; } }    //円周の中心
    [SerializeField]
    private float aroundTime; //一周する時間
    private float currentAngle; //現在の角度（-180～180で取る）
    [SerializeField]
    private float firstAngle;    //初期位置
    [SerializeField]
    private Vector2 radius = new Vector2(1, 1); //円周の距離
    [SerializeField]
    private bool clockwise; //時計回りか？

    private void Start()
    {
        transform.position = CenterPoint + new Vector2(Mathf.Cos(firstAngle) * radius.x, Mathf.Sin(firstAngle) * radius.y);
        currentAngle = firstAngle;
    }
    void Update()
    {
        if (clockwise)
        {
            currentAngle -= 360 / aroundTime * Time.deltaTime;
        }
        else
        {
            currentAngle += 360 / aroundTime * Time.deltaTime;
        }
        currentAngle = AngleCorrect(currentAngle);
        transform.position = CenterPoint + new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad) * radius.x, Mathf.Sin(currentAngle * Mathf.Deg2Rad) * radius.y);
    }

    private float AngleCorrect(float a)
    {
        a += 180;
        a %= 360;
        if (a < 0)
        {
            a += 180;
        }
        else
        {
            a -= 180;
        }
        return a;
    }
}