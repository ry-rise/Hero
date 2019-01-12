using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour {


    [SerializeField, Range(0, 1)]
    float Volume;
    AudioSource Bgm;
    public void VolumeChanger(float volume)
    {
        Bgm.volume = Volume * volume;
    }
    // Use this for initialization
    void Start () {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().BgmList.Add(this);
        Bgm = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
