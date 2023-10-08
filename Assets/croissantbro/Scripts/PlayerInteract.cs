/*
 Тут код намного лучше, но надо придумать как вызвать действие(удаление мусора) после окончания корутины
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private float _interactTime = 2f;
    public Image FillArea;

    private bool _isInteracting = false;
    private float _currentInteractTime = 0f;
    private bool isDone;
    void Start()
    {

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out LitterInteractable interactable))
                {
                    _isInteracting = true;
                    StartCoroutine(InteractCoroutine());
                    if (isDone)
                    {
                        interactable.Interact();
                        isDone = !isDone;
                    }
                    
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.E)||isDone)
        {
            _isInteracting = false;
            StopCoroutine(InteractCoroutine());
            _currentInteractTime = 0f;
            FillArea.fillAmount = 0f;
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
 */


// А этот код хотя бы работает
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private float _interactTime = 2f;
    public Image FillArea;

    private bool _isInteracting = false;
    private float _currentInteractTime = 0f;
    private Coroutine interactCoroutine; 
    private LitterInteractable interactable; 
    private bool isDone = false;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            interactable = null; 
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out LitterInteractable interact))
                {
                    interactable = interact;
                    _isInteracting = true;
                    interactCoroutine = StartCoroutine(InteractCoroutine()); 
                }
            }
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
                    interactable.Interact(); 
                    isDone = false;
                }
            }
            _currentInteractTime = 0f;
            FillArea.fillAmount = 0f;
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


