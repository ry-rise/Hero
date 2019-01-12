using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeManager : MonoBehaviour {

    [SerializeField, Range(0, 1)]
    float Volume;
    public
    AudioSource Se;

 
    public void VolumeChanger(float volume)
    {
        Se.volume = Volume* volume;
    }

    // Use this for initialization
    void Start () {
        Se = GetComponent<AudioSource>();
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SeList.Add(this);       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
