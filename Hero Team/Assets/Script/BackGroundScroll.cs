using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour {
    public bool ScrollFlag = false;
    private bool CanvasFlag = false;
    private float timer = 0.0f;
    private float CanvasTimer = 0.0f;
    [SerializeField]
    float EndTime;
    [SerializeField]
    GameObject ScrollCanvas;
	// Use this for initialization
    public void Scroll()
    {

            float scroll = Mathf.Repeat(Time.time * 0.2f, 1);
            Vector2 offset = new Vector2(0, scroll);
            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //ScrollFlagがtrueならEndTime秒間画面をスクロールする
        if(ScrollFlag == true)
        {
            timer += Time.deltaTime;
            Scroll();
            if(timer > EndTime)
            {
                timer = 0.0f;
                ScrollFlag = false;
                CanvasFlag = true;
                ScrollCanvas.SetActive(true);
               
            }
        }
        //画面のスクロール処理後にWarning演出をする
        if (CanvasFlag == true){
            CanvasTimer += Time.deltaTime;
            if (CanvasTimer > 5)
            {
                CanvasTimer = 0.0f;
                ScrollCanvas.SetActive(false);
                CanvasFlag = false;
            }
        }

    }
}
