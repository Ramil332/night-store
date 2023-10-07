using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

    public class PlayerMovement : MonoBehaviour
    {

    [SerializeField] private float _moveSpeed;
    private Controls _controls;

    private void Awake()
    {
        _controls = new Controls();
    }
    private void OnEnable()
    {
        _controls.Enable();
    }
    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Update()
    {
        Vector2 moveDirection = _controls.Main.Move.ReadValue<Vector2>();
        Move(moveDirection);
    }
    
    private void Move(Vector2 directionMove) 
    {
        Vector3 direction = new Vector3(directionMove.x, 0, directionMove.y);
        transform.position += direction * _moveSpeed * Time.deltaTime;
    }

}
