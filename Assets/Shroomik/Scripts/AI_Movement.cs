using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    private GameObject _cashRegister;
    private GameObject _endPoint;

    [Header("Время ожидания на кассе")]
    [SerializeField] private float _waitTime;

    private float _timeLeft;

    private bool _isServed;
    private bool _isWaitingServise;
    private bool _isLeaving;
    private bool _isHappy;
    public bool IsWaiting()
    {
        return _isWaitingServise;
    }

    private void Start()
    {
        _cashRegister = GameObject.Find("CashRegister");
        _endPoint = GameObject.Find("Exit");
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _isServed = false;
        _isWaitingServise = false;
        _timeLeft = 0f;
    }

    private void Update()
    {
        if (_isServed)
        {
            _isHappy = true;
            _agent.SetDestination(_endPoint.transform.position);
        }
        else
        {
            _agent.SetDestination(_cashRegister.transform.position);
        }

        if (!_isServed && _isLeaving)
        {
            _isHappy = false;

            _isWaitingServise = false;
            _agent.SetDestination(_endPoint.transform.position);
        }

        //if (_isServed && !_isWaitingServise)
        //{
        //    _agent.SetDestination(_endPoint.transform.position);
        //}

        if (_isWaitingServise)
        {
            if (_timeLeft <= 0)
            {
                _isServed = false;
                _isLeaving = true;
            }
        }
        _animator.SetFloat("Speed", _agent.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CashReg"))
        {
            ICustomer customer = other.gameObject.GetComponent<ICustomer>();
            if (customer != null)
            {
                customer.Approuched(_waitTime);
            }

            _timeLeft = _waitTime;
            _isWaitingServise = true;
            StartCoroutine(WaitingForService());
        }
        if (other.CompareTag("Exit") && _isLeaving)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ICustomer customer = other.gameObject.GetComponent<ICustomer>();
        if (customer != null)
        {
            customer.Leave(_isHappy);
        }
    }

    private IEnumerator WaitingForService()
    {
        while (_isWaitingServise && _timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            _animator.SetFloat("Speed", 0f);

            yield return null;
        }

    }

    public void CustomerServed()
    {
        StopCoroutine(WaitingForService());

        _isServed = true;
        _isWaitingServise = false;
        _isLeaving = true;
    }
}
