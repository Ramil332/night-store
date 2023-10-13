using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CashReg : MonoBehaviour, ICustomer
{
    public static Action <float> OnCustomerApprouched;
    public static Action<int> OnCustomerLeaveHappy;
    public static Action<int> OnCustomerLeaveUnHappy;

    [SerializeField] [Range(0, 1000)] private int _penalty;
    [SerializeField] [Range(0, 1000)] private int _reward;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _ring, _money;

    private float _waitTime;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Customer"))
    //    {
    //        OnCustomerApprouched?.Invoke();
    //    }
    //}

    public void Approuched(float time)
    {
        _waitTime = time;
        OnCustomerApprouched?.Invoke(_waitTime);
        _audioSource.clip = _ring;
        _audioSource.Play();

    }

    public void Leave(bool served)
    {
        if (served)
        {
            OnCustomerLeaveHappy?.Invoke(_reward);
            _audioSource.clip = _money;
            _audioSource.Play();
        }
        else
        {
            OnCustomerLeaveUnHappy?.Invoke(_penalty);
        }
    }
}
