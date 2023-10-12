using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalEvents : MonoBehaviour
{
    [Header("������������ ������ � ���")]
    [SerializeField] [Range(0, 900)] private float _nightDuration;

    [Header("��� ����� �� ���� ������� � ���")]
    [SerializeField] [Range(0, 300)] private float _minNextEventTime;
    [Header("���� ����� �� ���� ������� � ���")]
    [SerializeField] [Range(0, 300)] private float _maxNextEventTime;

    public static Action OnCustomerSpawn;
    public static Action OnGeneratorBroke;


    private int _nightTick;
    private int _nextEventTick;
    private int _eventNum;

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

        int nextEventTime = (int)UnityEngine.Random.Range(_minNextEventTime, _maxNextEventTime);
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
        _eventNum = UnityEngine.Random.Range(0, 2); // ����� �������� ��� ����� ����� �������
        switch (_eventNum)
        {
            case 0:
                OnCustomerSpawn?.Invoke();
                break;

            case 1:
                OnGeneratorBroke?.Invoke();
                break;

            default:
                print("No events");
                break;
        }
        Debug.Log("Event happend");
    }
}
