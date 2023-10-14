using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    [SerializeField] private LayerMask _doorMask;

    Vector3 velocity;

    public bool PlayerMove = true;

    public static Action OnDoorInteract;
    private void Start()
    {


    }



    void Update()
    {
        if (PlayerMove)
        {

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);

        }
        if (Input.GetKeyDown(KeyCode.J))
        {

        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 2f, _doorMask))
        {
            if (hit.collider != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnDoorInteract?.Invoke();
                }
            }
        }
    }

}