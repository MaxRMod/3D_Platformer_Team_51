using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class OptionsMenu : MonoBehaviour
{

    //Fullscreen
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //Quality



    public AudioMixer volumeMixer;
    //Volume
    public void SetVolume(float volume)
    {
        volumeMixer.SetFloat("volume", volume);
    }
}
