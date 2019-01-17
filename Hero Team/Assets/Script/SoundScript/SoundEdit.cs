using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEdit : MonoBehaviour
{
    [SerializeField]
    private Slider SeSlider;
    [SerializeField]
    private Slider BgmSlider;
    private float SeVolume;
    private float BgmVolume;
    // Use this for initialization
    void Start()
    {
        SeSlider.value = 1;
        BgmSlider.value = 1;
        SeVolume = SeSlider.value;

    }

    // Update is called once per frame
    void Update()
    {
        if (SeVolume != SeSlider.value)
        {
            SeVolume = SeSlider.value;
            SoundManager.SeVolumeChanger(SeVolume);
        }
        if (BgmVolume != BgmSlider.value)
        {
            SoundManager.BgmVolumeChanger(BgmVolume);
            BgmVolume = BgmSlider.value;
        }
    }
}