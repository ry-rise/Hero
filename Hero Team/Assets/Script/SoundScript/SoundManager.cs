using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private List<SeManager> seList = new List<SeManager>();
    public List<SeManager> SeList { get { return seList; } set { seList = value; } }
    private List<BgmManager> bgmList = new List<BgmManager>();
    public List<BgmManager> BgmList { get { return bgmList; } set { bgmList = value; } }
    public static float SeVolume;
    public static float BgmVolume;

    public static void SeVolumeChanger(float volume)
    {
        SeVolume = volume;
    }
    public static void BgmVolumeChanger(float volume)
    {
        BgmVolume = volume;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	 foreach(SeManager se in seList)
        {
            se.VolumeChanger(SeVolume);
        }
        foreach (BgmManager bgm in bgmList)
        {
            bgm.VolumeChanger(BgmVolume);
        }
    }
}
