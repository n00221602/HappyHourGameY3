using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SettingsMenu : MonoBehaviour

{
public bool initialized = false;
public AudioMixer mainMixer;
public Slider mouseSensetivitySlider;

void Start(){
    if (PlayerPrefs.HasKey("Sensetivity"))
    {
        mouseSensetivitySlider.value = PlayerPrefs.GetFloat("Sensetivity");
    }
    initialized = true;
}

    // Start is called before the first frame update
public void SetFullscreen(bool isFullscreen)
{
Screen.fullScreen = isFullscreen;
}

public void SetQuality(int qualityIndex)
{
    QualitySettings.SetQualityLevel(qualityIndex);
}

public void SetVolume(float volume)
{
    mainMixer.SetFloat("volume",volume);
}

public void SetMouseSensetivity(float val)
{
    if(! initialized) return;
    if(! Application.isPlaying) return;
    PlayerPrefs.SetFloat("Sensetivity", val);


}
}
