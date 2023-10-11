using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPoints : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelPointsText;
    [SerializeField] [Range(0, 1000)] private int _levelPoints;

    private void OnEnable()
    {
        CashReg.OnCustomerLeaveHappy += Reward;
        CashReg.OnCustomerLeaveUnHappy += Penalty;
    }

    private void OnDisable()
    {
        CashReg.OnCustomerLeaveHappy -= Reward;
        CashReg.OnCustomerLeaveUnHappy -= Penalty;
    }

    private void Start()
    {
        _levelPointsText.text = _levelPoints.ToString();
    }

    private void Reward(int reward)
    {
        _levelPoints += reward;
        _levelPointsText.text = _levelPoints.ToString();
    }

    private void Penalty(int penalty)
    {
        _levelPoints -= penalty;
        _levelPointsText.text = _levelPoints.ToString();
    }
}
