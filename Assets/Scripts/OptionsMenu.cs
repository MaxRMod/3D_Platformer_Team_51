using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    //Fullscreen
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //Quality




    public AudioMixer volumeMixer;
    public Slider slider;
    //Volume

    void Start()
    {
        slider.value = GameData.Instance.SliderValue;
    }

    public void SetVolume(float volume)
    {
        volumeMixer.SetFloat("volume", volume);
        GameData.Instance.SliderValue = this.slider.value;
    }
}
