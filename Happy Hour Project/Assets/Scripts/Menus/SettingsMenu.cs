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
    //checks the current mouse sensitivity
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

//sets the video quality for the game
public void SetQuality(int qualityIndex)
{
    QualitySettings.SetQualityLevel(qualityIndex);
}

//allows user to alter the volume of the game
public void SetVolume(float volume)
{
    mainMixer.SetFloat("volume",volume);
}

//allows the user to edit the sensitivity of their mouse

public void SetMouseSensetivity(float val)
{
    if(! initialized) return;
    if(! Application.isPlaying) return;
    PlayerPrefs.SetFloat("Sensetivity", val);


}
}
