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

public IEnumerator SetLanguage()
    {
        // Wait for the localization system to initialize, loading Locales, preloading, etc.
        yield return LocalizationSettings.InitializationOperation;

        // This variable selects the language. For example, if in the table your first language is English then 0 = English. If the second language in the table is Russian then 1 = Russian etc.
        int i = 0;

// This part changes the language
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[i];
    }

    public AudioMixer volumeMixer;
    //Volume
    public void SetVolume(float volume)
    {
        volumeMixer.SetFloat("volume", volume);
    }
}
