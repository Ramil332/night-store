using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_interact : MonoBehaviour
{
    private bool _isDoorOpen;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        PlayerMovement.OnDoorInteract += DoorInteract;
    }
    private void OnDisable()
    {
        PlayerMovement.OnDoorInteract -= DoorInteract;
    }
    private void DoorInteract()
    {
        if (!_isDoorOpen)
        {
            _animator.SetBool("isOpened", true);
            _isDoorOpen = true;
        }
        else
        {
            _animator.SetBool("isOpened", false);
            _isDoorOpen = false;
        }
    }
}
