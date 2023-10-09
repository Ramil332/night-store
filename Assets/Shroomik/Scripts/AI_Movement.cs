using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    private NavMeshAgent _agent;
   // private Animator animator;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform _cashRegister;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _waitTime;

    private float _timeLeft;

    private bool _isServed;
    private bool _isWaitingServise;
    private bool _isLeaving;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _isServed = false;
        _isWaitingServise = false;
        _timeLeft = 0f;
    }

    private void Update()
    {
        if (_isServed)
        {
            _agent.SetDestination(_endPoint.position);
        }
        else
        {
            _agent.SetDestination(_cashRegister.position);
        }

        if (!_isServed && _isLeaving)
        {
            _agent.SetDestination(_endPoint.position);
        }

        if (_isServed && !_isWaitingServise)
        {
            _agent.SetDestination(_endPoint.position);
        }

        if (_isWaitingServise)
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                _isServed = false;
                _isLeaving = true;
            }
        }

    }    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CashReg"))
        {
            _timeLeft = _waitTime;
            _isWaitingServise = true;
        }
        if (other.CompareTag("Exit"))
        {
            Destroy(gameObject);
        }
    }

    public void CustomServed()
    {
        _isServed = true;
        _isWaitingServise = false;
    }
}
