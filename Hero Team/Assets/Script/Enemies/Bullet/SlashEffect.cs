using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour {
    private IEnumerator DelayMethod(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Destroy(this.gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine("DelayMethod", 0.3f);
    }
}
