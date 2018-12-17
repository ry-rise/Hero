using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour {
    public bool ScrollFlag = false;
    private float timer = 0.0f;
    [SerializeField]
    float EndTime;
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
        if(ScrollFlag == true)
        {
            timer += Time.deltaTime;
            Scroll();
            if(timer > EndTime)
            {
                timer = 0.0f;
                ScrollFlag = false;
               
            }
        }

    }
}
