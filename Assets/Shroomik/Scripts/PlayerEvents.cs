using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] private TMP_Text _customerText;
    [SerializeField] private TMP_Text _generatorText;

    [SerializeField] private GameObject _sellerPanel;
    [SerializeField] private GameObject _generatorPanel;

    [SerializeField] private LayerMask _customerMask;
    [SerializeField] private LayerMask _generatorMask;

    private bool _isProvidingService, _isFixingGen;

    private IEnumerator _coroutine;
    private void OnEnable()
    {
        CashReg.OnCustomerApprouched += CustomerNeedServise;
        _sellerPanel.SetActive(false);

        GlobalEvents.OnGeneratorBroke += GeneratorBroke;
        _customerText.gameObject.SetActive(false);
        _generatorText.gameObject.SetActive(false);
        Cursor.visible = false;
    }


    private void OnDisable()
    {
        CashReg.OnCustomerApprouched -= CustomerNeedServise;
        GlobalEvents.OnGeneratorBroke -= GeneratorBroke;

    }

    private void GeneratorBroke()
    {
        _generatorText.gameObject.SetActive(true);
    }

    private void CustomerNeedServise(float waitTime)
    {
        _coroutine = CustomerAlarm(waitTime);
        StartCoroutine(_coroutine);
    }

    private IEnumerator CustomerAlarm(float waitTime)
    {
        _customerText.gameObject.SetActive(true);

        yield return new WaitForSeconds(waitTime);
        _customerText.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CashReg"))
        {
            _isProvidingService = true;
        }
        if (other.CompareTag("Generator"))
        {
            _isFixingGen = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CashReg"))
        {
            _isProvidingService = true;
        }
        if (other.CompareTag("Generator"))
        {
            _isFixingGen = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        _isProvidingService = false;
        _isFixingGen = false;
    }

    private void Update()
    {
        ProvideService();
        RepairGenerator();
    }

    private void ProvideService()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, _customerMask)
              && _isProvidingService && hit.collider.GetComponent<AI_Movement>().IsWaiting())
        {
            var customer = hit;
            _sellerPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                customer.collider.GetComponent<AI_Movement>().CustomerServed();
                StopCoroutine(_coroutine);
                _customerText.gameObject.SetActive(false);

                _sellerPanel.SetActive(false);
            }
        }
        else
        {
            _sellerPanel.SetActive(false);
        }
    }

    private void RepairGenerator()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, _generatorMask)
             && _isFixingGen && hit.collider.GetComponent<Generator>().IsBroke())
        {

            var generator = hit;
            _generatorPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                _generatorPanel.SetActive(false);
                generator.collider.GetComponent<Generator>().GeneratorFixed();
                _generatorText.gameObject.SetActive(false);

            }
        }
        else
        {
            _generatorPanel.SetActive(false);
        }


    }
}
