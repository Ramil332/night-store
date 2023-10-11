using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] _musicSounds, _sfxSounds;
    public AudioSource _musicSource, _sfxSource;

    public static AudioManager Instance;


    void Awake()
    {
       // Cursor.visible = false;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            _sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
    }

    private void Start()
    {
        PlayMusic("Menu");
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(_musicSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        else
        {
            _musicSource.clip = s.clip;

            if (PlayerPrefs.HasKey("MusicVolume"))
            {
                _musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            }
            else
            {
                _musicSource.volume = s.volume;
            }
            _musicSource.pitch = s.pitch;
            _musicSource.loop = s.loop;
            _musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(_sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        else
        {
            _sfxSource.clip = s.clip;
            if (PlayerPrefs.HasKey("SFXVolume"))
            {
                _sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume");
            }
            else
            {
                _sfxSource.volume = s.volume;
            }
            _sfxSource.pitch = s.pitch;
            _sfxSource.priority = s.priority;
            _sfxSource.panStereo = s.stereoPan;

            _sfxSource.loop = s.loop;

            //_sfxSource.Play();

            _sfxSource.PlayOneShot(s.clip);
        }
    }
    public void StopSFX(string name)
    {
        Sound s = Array.Find(_sfxSounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
        else
        {
            _sfxSource.clip = s.clip;
            _sfxSource.Stop();

        }
    }
    public void MusicVolume(float volume)
    {
        _musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        _sfxSource.volume = volume;
    }

}
