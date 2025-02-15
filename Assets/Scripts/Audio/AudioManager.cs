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
    public AudioClip blueSlimeAttack;
    public AudioClip blackSlimeAttack;
    public AudioClip bossAttack;
    public AudioClip bossDash;
    public AudioClip bossFireLaser;
    public AudioClip bossFireHomingMissle;

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
        //Background music loop
        musicSource.loop = true;
        musicSource.Play();

        settingVolumeMenu.SetActive(false);
    }

    private void Update()
    {
        if (PlayerController.instance && PlayerController.instance.inputController.Gameplay.AudioSetting.WasPressedThisFrame())
        {
            AudioSetting();
        }
    }

    //Active audio setting
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

    //Change volume mucsic
    public void ChangeMusic(float value)
    {
        musicSource.volume = value;
    }

    //Change volume sfx
    public void ChangeSFX(float value)
    {
        SFXSource.volume = value;
    }

}
