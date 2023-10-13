using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)] public float volume;

        [Range(.1f, 3f)] public float pitch;
        [Range(-1f, 1f)] public float stereoPan;

        [Range(0, 256)] public int priority;


        public bool loop;
        public bool hasCooldown;
    }
    public Sound[] _musicSounds, _sfxSounds;
    public AudioSource _musicSource, _sfxSource;

    private void Start()
    {
        PlayMusic("Test");
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

}
