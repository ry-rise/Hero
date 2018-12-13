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
    private Wall[] walls;

    // Use this for initialization
    private void Start()
    {
        WallSetting();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void WallSetting()
    {
        GameObject backGround = GameObject.Find("BackGround");
        walls = new Wall[4];
        walls[(int)HitPointNumber.Top] = backGround.transform.Find("TopWall").GetComponent<Wall>();
        walls[(int)HitPointNumber.Bottom] = backGround.transform.Find("BottomWall").GetComponent<Wall>();
        walls[(int)HitPointNumber.Right] = backGround.transform.Find("RightWall").GetComponent<Wall>();
        walls[(int)HitPointNumber.Left] = backGround.transform.Find("LeftWall").GetComponent<Wall>();
        walls[(int)HitPointNumber.Bottom].GetComponent<BoxCollider2D>().isTrigger = true;
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