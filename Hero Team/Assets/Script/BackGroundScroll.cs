using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour {


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

    }
}
