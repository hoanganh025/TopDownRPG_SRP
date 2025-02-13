using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public Slider musicSlider;
    public Slider SFXSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVolume") && PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadAllVolume();
        }
        else
        {
            musicSlider.value = AudioManager.instance.musicSource.volume;
            SFXSlider.value = AudioManager.instance.SFXSource.volume;
        }
    }

    public void MusicVolume(float value)
    {
        AudioManager.instance.ChangeMusic(value);
    }

    public void SFXVolume(float value)
    {
        AudioManager.instance.ChangeSFX(value);
    }

    private void SetAllVolume()
    {
        float musicValue = musicSlider.value;
        float sfxValue = SFXSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicValue);
        PlayerPrefs.SetFloat("SFXVolume", sfxValue);
        PlayerPrefs.Save();
    }

    private void LoadAllVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    private void OnApplicationQuit()
    {
        SetAllVolume();
    }
}
