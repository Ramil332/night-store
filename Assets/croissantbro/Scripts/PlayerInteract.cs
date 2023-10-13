
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerInteract : MonoBehaviour
{
    private float _interactTime = 2f;
    public Image FillArea;
    private bool _isInteracting = false;
    private float _currentInteractTime = 0f;
    private Coroutine interactCoroutine; 
    private ICollectable interactable; 
    private bool isDone = false;
    [SerializeField] private GameObject _garbagePanel;
    [SerializeField] private LayerMask _garbageMask;

    public static Action OnCollectTrash;
    

    public void Update()
    {
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit trashHit, 2f, _garbageMask))
        {
            ICollectable collectable = trashHit.collider.GetComponent<ICollectable>();
            if (collectable != null)
            _garbagePanel.SetActive(true);
            
        }
        else _garbagePanel.SetActive(false);
        GarbageCollect();
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
            OnCollectTrash?.Invoke();

        }
    }
    private void GarbageCollect()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;
            //Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity, _garbageMask))
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


