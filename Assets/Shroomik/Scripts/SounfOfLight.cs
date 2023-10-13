using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SounfOfLight : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;
        _audioSource.Play();
    }
    private void OnEnable()
    {
        GlobalEvents.OnGeneratorBroke += GeneratorBroke;
        Generator.OnGeneratorFixed += GeneratorFixed;
    }

    private void OnDisable()
    {
        GlobalEvents.OnGeneratorBroke -= GeneratorBroke;
        Generator.OnGeneratorFixed -= GeneratorFixed;

    }
    private void GeneratorBroke()
    {
        _audioSource.Stop();

    }

    public void GeneratorFixed()
    {
        _audioSource.Play();
    }

}
