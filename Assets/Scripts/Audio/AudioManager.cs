using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    public bool isSettingVolumeActived = false;
    public GameObject settingVolumeMenu;

    [Header("Audio Source")]
    public AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip slashSword;
    public AudioClip magicShoot;
    public AudioClip magicExplore;
    public AudioClip playerHurt;
    public AudioClip playerDash;
    public AudioClip playerFootStep;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        settingVolumeMenu.SetActive(false);
    }

    private void Update()
    {
        if (PlayerController.instance.inputController.Gameplay.AudioSetting.WasPressedThisFrame())
        {
            AudioSetting();
        }
    }

    private void AudioSetting()
    {
        if (!isSettingVolumeActived)
        {
            Time.timeScale = 0;
            isSettingVolumeActived = true;
            settingVolumeMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            isSettingVolumeActived = false;
            settingVolumeMenu.SetActive(false);
        }
    }

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void ChangeMusic(float value)
    {
        musicSource.volume = value;
    }
    public void ChangeSFX(float value)
    {
        SFXSource.volume = value;
    }

}
