using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents : MonoBehaviour
{

    [SerializeField] [Range(0, 300)] private float _nightDuration;

    [Header("Мин время до след события")]
    [SerializeField] [Range(0, 300)] private float _minNextEventTime;
    [Header("Макс время до след события")]
    [SerializeField] [Range(0, 300)] private float _maxNextEventTime;

    private int _nightTick;
    private int _nextEventTick;

    private void Start()
    {
        Timer.OnTick += Timer_OnTick;
        _nightTick = 0;
        _nextEventTick = 0;
    }

    private void Timer_OnTick(object sender, Timer.OnTickEventArgs e)
    {
        _nightTick++;
        _nextEventTick++;

        if (_nightTick >= _nightDuration)
        {
            GameOver();
            _nightTick = 0;
        }

        int nextEventTime = (int)Random.Range(_minNextEventTime, _maxNextEventTime);
        if (_nextEventTick >= nextEventTime)
        {
            _nextEventTick = 0;
            CreateEvent();
        }

    }

    private void GameOver()
    {
        Debug.Log("GameOver");
    }

    private void CreateEvent()
    {
        Debug.Log("Event happend");
    }
}
