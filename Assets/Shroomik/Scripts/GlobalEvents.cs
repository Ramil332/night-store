using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEvents : MonoBehaviour
{
    [Header("ƒлительность уровн€ в сек")]
    [SerializeField] [Range(0, 900)] private float _nightDuration;

    [Header("ћин врем€ до след событи€ в сек")]
    [SerializeField] [Range(0, 300)] private float _minNextEventTime;
    [Header("ћакс врем€ до след событи€ в сек")]
    [SerializeField] [Range(0, 300)] private float _maxNextEventTime;

    [SerializeField] private GameObject _gameOverPanel;

    public static Action OnCustomerSpawn;
    public static Action OnGeneratorBroke;
    public static Action OnSpawnLitter;
    public static Action OnItemsFall;


    private int _nightTick;
    private int _nextEventTick;
    private int _eventNum;

    private void OnEnable()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    private void Start()
    {
        _gameOverPanel.SetActive(false);

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

        int nextEventTime = (int)UnityEngine.Random.Range(_minNextEventTime, _maxNextEventTime);
        if (_nextEventTick >= nextEventTime)
        {
            _nextEventTick = 0;
            CreateEvent();
        }

    }

    private void GameOver()
    {
        _gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    private void CreateEvent()
    {
        _eventNum = UnityEngine.Random.Range(0, 4); // здесь максимум это число общих событий
        switch (_eventNum)
        {
            case 0:
                OnCustomerSpawn?.Invoke();
                break;

            case 1:
                OnItemsFall?.Invoke();
                break;

            case 2:
                OnSpawnLitter?.Invoke();
                break;

            case 3:
                OnGeneratorBroke?.Invoke();
                break;


            

            default:
                print("No events");
                break;
        }
    }
}
