using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject _lights;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    private bool _isBroken;

    public static Action OnGeneratorFixed;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
    public bool IsBroke()
    {
        return _isBroken;
    }
    private void OnEnable()
    {
        GlobalEvents.OnGeneratorBroke += GeneratorBroke;
        _lights.SetActive(true);
    }

    private void OnDisable()
    {
        GlobalEvents.OnGeneratorBroke -= GeneratorBroke;
    }

    private void GeneratorBroke()
    {
        _lights.SetActive(false);
        _isBroken = true;
        _audioSource.Stop();

    }

    public void GeneratorFixed()
    {
        _isBroken = false;
        _lights.SetActive(true);
        _audioSource.Play();

        OnGeneratorFixed?.Invoke();
    }
}
