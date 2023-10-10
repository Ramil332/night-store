using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastOutline : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private float _rayDistance = 4f;
    private Outline _lastOulinedObject;
    private void Start()
    {
        
    }
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _rayDistance))
        {
            if (hit.transform.gameObject.CompareTag("Product")|| hit.transform.gameObject.CompareTag("Litter"))
            {
                if(_lastOulinedObject != null)
                    _lastOulinedObject.enabled = false;
                _lastOulinedObject = hit.transform.gameObject.GetComponent<Outline>();
                _lastOulinedObject.enabled = true;
            }
        }
    }
}
