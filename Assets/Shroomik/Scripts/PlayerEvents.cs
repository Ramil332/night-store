using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerEvents : MonoBehaviour
{
    [SerializeField] private TMP_Text _customerText;
    [SerializeField] private TMP_Text _generatorText;
    [SerializeField] private TMP_Text _garbageText;

    [SerializeField] private GameObject _sellerPanel;
    [SerializeField] private GameObject _generatorPanel;

    [SerializeField] private LayerMask _customerMask;
    [SerializeField] private LayerMask _generatorMask;

    private bool _isProvidingService, _isFixingGen;
    private bool _isInteracting = false;
    private float _interactTime = 2f;
    private float _currentInteractTime = 0f;

    private Coroutine interactCoroutine;
    private bool isDone = false;
    private IEnumerator _coroutine;
    public Image FillArea;
    private void OnEnable()
    {
        CashReg.OnCustomerApprouched += CustomerNeedServise;
        _sellerPanel.SetActive(false);
        PlayerInteract.OnCollectTrash += CollectLitter;
        GlobalEvents.OnGeneratorBroke += GeneratorBroke;
        GlobalEvents.OnSpawnLitter += SpawnLitter;
        _customerText.gameObject.SetActive(false);
        _generatorText.gameObject.SetActive(false);
        _garbageText.gameObject.SetActive(false);
        Cursor.visible = false;
    }


    private void OnDisable()
    {
        CashReg.OnCustomerApprouched -= CustomerNeedServise;
        GlobalEvents.OnGeneratorBroke -= GeneratorBroke;
        PlayerInteract.OnCollectTrash -= CollectLitter;
        GlobalEvents.OnSpawnLitter -= SpawnLitter;
    }

    private void SpawnLitter()
    {
        _garbageText.gameObject.SetActive(true);
    }
    private void CollectLitter() { 
        _garbageText.gameObject.SetActive(false) ;
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
                _isInteracting = true;
                interactCoroutine = StartCoroutine(InteractCoroutine());

            }
            if (Input.GetKeyUp(KeyCode.E) || isDone)
            {
                _isInteracting = false;
                if (interactCoroutine != null)
                {
                    StopCoroutine(interactCoroutine);
                    interactCoroutine = null;

                    if (generator.collider != null && isDone)
                    {
                        _generatorPanel.SetActive(false);
                        generator.collider.GetComponent<Generator>().GeneratorFixed();
                        _generatorText.gameObject.SetActive(false);
                        isDone = false;
                    }
                }
                _currentInteractTime = 0f;
                FillArea.fillAmount = 0f;
            }
        }
        else
        {
            _generatorPanel.SetActive(false);
        }


    }
    private IEnumerator InteractCoroutine()
    {
        while (_isInteracting && _currentInteractTime < _interactTime)
        {
            _currentInteractTime += Time.deltaTime;
            float fillAmount = _currentInteractTime / _interactTime;
            FillArea.fillAmount = fillAmount;
            yield return null;
        }
        if (_isInteracting)
        {
            isDone = true;
            

        }
    }
}
