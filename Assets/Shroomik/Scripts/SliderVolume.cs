using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1);
            LoadMusic();
        }
        else
        {
            LoadMusic();
        }

        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1);
            LoadSFX();
        }
        else
        {
            LoadSFX();
        }
    }

    public void SetMusicVolume()
    {
        AudioManager.Instance.MusicVolume(_volumeSlider.value);
        SaveMusic();
    }
    public void SetSFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
        SaveSFX();
    }
    public void LoadMusic()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    public void LoadSFX()
    {
        _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void SaveMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", _volumeSlider.value);
    }
    public void SaveSFX()
    {
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
    }
}
