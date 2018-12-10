using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum HitPointNumber
{
    Top,
    Bottom,
    Left,
    Right,
}

[System.Flags]
public enum HitPointFlag
{
    None = 0,
    Top = 1 << HitPointNumber.Top,
    Bottom = 1 << HitPointNumber.Bottom,
    Left = 1 << HitPointNumber.Left,
    Right = 1 << HitPointNumber.Right,
}

public class WallHitter : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField]
    private GameObject wallPrefab;
    private Wall[] walls;


    // Use this for initialization
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        WallSetting();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void WallSetting()
    {
        walls = new Wall[4];
        for (int n = 0; n < walls.Length; ++n)
        {
            walls[n] = Instantiate(wallPrefab).GetComponent<Wall>();
        }
        walls[(int)HitPointNumber.Top].transform.position = GetScreenTop();
        walls[(int)HitPointNumber.Bottom].transform.position = GetScreenBottom();
        walls[(int)HitPointNumber.Left].transform.position = GetScreenLeft();
        walls[(int)HitPointNumber.Right].transform.position = GetScreenRight();

        walls[(int)HitPointNumber.Top].transform.localScale = new Vector2(100, 0.1f);
        walls[(int)HitPointNumber.Bottom].transform.localScale = new Vector2(100, 0.1f);
        walls[(int)HitPointNumber.Left].transform.localScale = new Vector2(0.1f, 100);
        walls[(int)HitPointNumber.Right].transform.localScale = new Vector2(0.1f, 100);

        walls[(int)HitPointNumber.Bottom].GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private Vector3 GetScreenTop()
    {
        return new Vector2((GetScreenTopLeft().x + GetScreenBottomRight().x) / 2, GetScreenTopLeft().y);
    }
    private Vector3 GetScreenBottom()
    {
        return new Vector2((GetScreenTopLeft().x + GetScreenBottomRight().x) / 2, GetScreenBottomRight().y);
    }
    private Vector3 GetScreenLeft()
    {
        return new Vector2(GetScreenTopLeft().x, (GetScreenTopLeft().y + GetScreenBottomRight().y) / 2);
    }
    private Vector3 GetScreenRight()
    {
        return new Vector2(GetScreenBottomRight().x, (GetScreenTopLeft().y + GetScreenBottomRight().y) / 2);
    }

    private Vector3 GetScreenTopLeft()
    {
        // 画面の左上を取得
        Vector2 topLeft = mainCamera.ScreenToWorldPoint(Vector2.zero);
        // 上下反転させる
        topLeft.Scale(new Vector2(1f, -1f));
        return topLeft;
    }

    private Vector3 GetScreenBottomRight()
    {
        // 画面の右下を取得
        Vector2 bottomRight = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        // 上下反転させる
        bottomRight.Scale(new Vector2(1f, -1f));
        return bottomRight;
    }

    //この当たり判定、使えるなら使ってみろ
    public bool IsHit(GameObject myObject, HitPointFlag hitPoint = ~HitPointFlag.None, bool andOperation = false)
    {
        HitPointFlag hpf = 0;
        for (int n = 0; n < 4; ++n)
        {
            if (((int)hitPoint & (1 << n)) != (1 << n)) continue;
            if (!walls[n].Search(myObject)) continue;
            hpf += 1 << n;
        }

        //AND演算
        if (andOperation)
        {
            for (int n = 0; n < 4; ++n)
            {
                if (((int)hitPoint & (1 << n)) != (1 << n)) continue;
                if (((int)hpf & (1 << n)) == (1 << n)) continue;
                return false;
            }
            return true;
        }
        //OR演算
        else
        {
            for (int n = 0; n < 4; ++n)
            {
                if (((int)hitPoint & (1 << n)) != (1 << n)) continue;
                if (((int)hpf & (1 << n)) != (1 << n)) continue;
                return true;
            }
            return false;
        }
    }
}