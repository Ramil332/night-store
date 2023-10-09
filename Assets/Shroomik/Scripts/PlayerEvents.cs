using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerEvents : MonoBehaviour
{

    [SerializeField] private GameObject _customerEventsPanel;
    [SerializeField] private GameObject _sellerPanel;
    [SerializeField] private LayerMask _customerMask;

    private bool _isProvidingService;

    private void OnEnable()
    {
        CashReg.OnCustomerApprouched += CustomerNeedServise;
        _customerEventsPanel.gameObject.SetActive(false);
        _sellerPanel.SetActive(false);

    }


    private void OnDisable()
    {
        CashReg.OnCustomerApprouched -= CustomerNeedServise;
    }


    private void CustomerNeedServise()
    {
        _customerEventsPanel.gameObject.SetActive(true);
        StartCoroutine(CustomerAlarm());
    }

    private IEnumerator CustomerAlarm()
    {
        yield return new WaitForSeconds(2f);
        _customerEventsPanel.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CashReg"))
        {
            _isProvidingService = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isProvidingService = false;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, _customerMask) 
           && _isProvidingService)
        {
            if (hit.collider.GetComponent<AI_Movement>().IsWaiting())
            {
                var customer = hit;
                _sellerPanel.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    customer.collider.GetComponent<AI_Movement>().CustomerServed();
                    _sellerPanel.SetActive(false);
                }

            }
        }
    }
}
