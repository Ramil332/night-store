using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProductFall : MonoBehaviour
{
    [SerializeField] private Transform[] _products;
    private int _num = 0;

    private float _interactTime = 2f;
    public Image FillArea;
    private bool _isInteracting = false;
    private float _currentInteractTime = 0f;
    private Coroutine interactCoroutine;
   
    private bool isDone = false;
    [SerializeField] private GameObject _garbagePanel;
    [SerializeField] private LayerMask _productMask;

    public static Action OnProductsPickUp;

    private void OnEnable()
    {
        GlobalEvents.OnItemsFall += FallProducts;
    }

    private void OnDisable()
    {
        GlobalEvents.OnItemsFall -= FallProducts;

    }

    public void FallProducts()
    {


        Vector3 currentRotation = _products[_num].transform.eulerAngles;
        _products[_num].transform.eulerAngles = new Vector3(-30, currentRotation.y, currentRotation.z);
        _num++;



    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f, _productMask))
            {
                ICollectable collectable = hit.collider.GetComponent<ICollectable>();
                if (collectable != null)
                {
                    // The player is looking at a collectable product
                    collectable.Collect();
                }
            }
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
            OnProductsPickUp?.Invoke();

        }
    }
    private void ItemPickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // float interactRange = 2f;
            //Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, _productMask))
            {
                interactable = hit.collider.GetComponent<ICollectable>();
                if (interactable != null)
                {
                    //interactable.Collect();
                    _isInteracting = true;
                    interactCoroutine = StartCoroutine(InteractCoroutine());
                }
            }
            //interactable = null; 

            //foreach (Collider collider in colliderArray)
            //{
            //    if (collider.TryGetComponent(out LitterInteractable interact))
            //    {

            //    }
            //}
        }
        if (Input.GetKeyUp(KeyCode.E) || isDone)
        {
            _isInteracting = false;
            if (interactCoroutine != null)
            {
                StopCoroutine(interactCoroutine);
                interactCoroutine = null;

                if (interactable != null && isDone)
                {

                    interactable.Collect();
                    isDone = false;
                }
            }
            _currentInteractTime = 0f;
            FillArea.fillAmount = 0f;
        }




    }
}
