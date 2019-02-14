using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {
    [SerializeField]
    AudioSource Tap;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void OnClick()
    {
        Tap.Play();
        GameManager.ScoreReset();
        GameManager.StageNumber = 1;
        SceneManager.LoadScene("Stage1");
    }
}
