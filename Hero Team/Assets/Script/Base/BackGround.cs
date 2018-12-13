using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private const float windowWidth = 1440;
    private const float windowHeight = 2560;
    private Camera mainCamera;
    // Use this for initialization
    /*void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        transform.localScale *= Setting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    float Setting()
    {
        //標準の画面比率
        float developAspect = windowWidth / windowHeight;
        //実機の画面比率
        float deviceAspect = (float)Screen.width / Screen.height;
        //対比
        float scale = deviceAspect / developAspect;
        return 1.0f / scale;
    }*/
}