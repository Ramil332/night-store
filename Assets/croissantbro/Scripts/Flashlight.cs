using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool isOn = false;
    public GameObject light;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isOn = !isOn; 
            _audioSource.Play();
            if (isOn)
            light.SetActive(true); 
            else light.SetActive(false);
        }
    }
}
