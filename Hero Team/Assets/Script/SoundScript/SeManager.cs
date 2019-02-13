using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeManager : MonoBehaviour
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
    private AudioSource se;

    private int clipNumber;

    public void SeChanger(int number)
    {
        if (clipNumber == number) return;
        clipNumber = number;
        se.clip = controller[clipNumber].Clip;
        VolumeChanger();
    }

    private void VolumeChanger()
    {
        se.volume = controller[clipNumber].Volume * SoundManager.SeVolume;
    }

    public void Play()
    {
        if (se.loop)
        {
            se.loop = false;
        }
        se.Play();
    }

    public void Stop()
    {
        se.Stop();
    }

    public bool End()
    {
        if (!se.isPlaying)
        {
            return true;
        }
        return false;
    }

    // Use this for initialization
    void Awake()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().SeList.Add(this);
        se = GetComponent<AudioSource>();
        clipNumber = 0;
        VolumeChanger();
    }

    // Update is called once per frame
    void Update()
    {

    }
}