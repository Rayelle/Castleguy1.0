using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Video;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider volumeSlider;
    float minValue = -60;


    private void Start()
    {
        float volumeValue;
        mixer.GetFloat("SoundVolume", out volumeValue);
        volumeSlider.value = 1 - volumeValue / minValue;
        //volumeSlider.value = Mathf.Pow(10, volumeValue) / 20 ;
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("SoundVolume", Mathf.Lerp(minValue, 0, sliderValue));
        //mixer.SetFloat("SoundVolume", Mathf.Log10 (sliderValue) * 20);
        //mixer.SetFloat("SoundVolume", 10 *sliderValue);
    }

}
