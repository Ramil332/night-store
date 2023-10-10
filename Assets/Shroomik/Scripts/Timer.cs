using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public class OnTickEventArgs : EventArgs
    {
        public int Tick;
    }

    public static event EventHandler<OnTickEventArgs> OnTick;

    private const float PAY_TIMER = 1f;

    private int _tick;

    private float _tickTimer;

    private void Awake()
    {
        _tick = 0;
    }

    private void Update()
    {
        _tickTimer += Time.deltaTime;

        if (_tickTimer >= PAY_TIMER)
        {
            _tickTimer -= PAY_TIMER;
            _tick++;

            OnTick?.Invoke(this, new OnTickEventArgs { Tick = _tick });

        }
    }
}