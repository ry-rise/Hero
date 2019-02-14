using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    [System.Serializable]
    private class SoundController
    {
        [SerializeField]
        private AudioClip clip;
        public AudioClip Clip { get { return clip; } }
        [SerializeField, Range(0f, 1f)]
        private float volume;
        public float Volume { get { return volume; } }
    }
    [SerializeField]
    private List<SoundController> controller;
    private AudioSource bgm;

    private int clipNumber;

    public void BgmChanger(int number)
    {
        if (clipNumber == number) return;
        clipNumber = number;
        bgm.clip = controller[clipNumber].Clip;
        VolumeChanger();
    }

    private void VolumeChanger()
    {
        bgm.volume = controller[clipNumber].Volume * SoundManager.BgmVolume;
    }

    public void Play()
    {
        if (!bgm.loop)
        {
            bgm.loop = true;
        }
        bgm.Play();
    }

    public void Stop()
    {
        bgm.Stop();
    }

    // Use this for initialization
    void Awake()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().BgmList.Add(this);
        bgm = GetComponent<AudioSource>();
        clipNumber = -1;
        BgmChanger(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}