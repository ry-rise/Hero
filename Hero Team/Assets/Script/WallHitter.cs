using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallHitter : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;    //映っているか半手数rカメラ参照
    [SerializeField]
    private GameObject wallPrefab;
    private GameObject [] wallObjects;
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
        wallObjects = new GameObject[4];
        wallObjects[0] = Instantiate(wallPrefab, new Vector2(GetScreenTopLeft().x, GetScreenTopLeft().y), Quaternion.identity);
        wallObjects[1] = Instantiate(wallPrefab, new Vector2(GetScreenTopLeft().x, GetScreenBottomRight().y), Quaternion.identity);
        wallObjects[2] = Instantiate(wallPrefab, new Vector2(GetScreenBottomRight().x, GetScreenTopLeft().y), Quaternion.identity);
        wallObjects[3] = Instantiate(wallPrefab, new Vector2(GetScreenBottomRight().x, GetScreenBottomRight().y), Quaternion.identity);
    }

    private Vector3 GetScreenTopLeft()
    {
        // 画面の左上を取得
        Vector3 topLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        // 上下反転させる
        topLeft.Scale(new Vector3(1f, -1f, 1f));
        return topLeft;
    }

    private Vector3 GetScreenBottomRight()
    {
        // 画面の右下を取得
        Vector3 bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        // 上下反転させる
        bottomRight.Scale(new Vector3(1f, -1f, 1f));
        return bottomRight;
    }

    public bool IsHit(GameObject myObject)
    {
        return false;
    }
}