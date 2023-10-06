using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace daw
{

    public class PlayerMovement : MonoBehaviour
    {

        public CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f;
        public float jumpHeight = 3f;


        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
        Vector3 velocity;
        bool isGrounded;
        public bool PlayerMove = true;
        public AudioClip soundClip;
        private AudioSource audioSource;

        public GameObject pause;
        public GameObject crosshair;
        public GameObject left;
        public GameObject right;
        private bool isPaused = false;


        void Start()
        {
        }

        
      
        void Update()
        {
            if (isPaused == false && Input.GetKeyDown(KeyCode.Escape))
            {
                pause.SetActive(true);
                crosshair.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                Time.timeScale = 0f;
                PlayerMove = false;
                Cursor.lockState = CursorLockMode.None;
                isPaused = true;

            }
            else if (isPaused == true && Input.GetKeyDown(KeyCode.Escape))
            {
                pause.SetActive(false);
                crosshair.SetActive(true);
                left.SetActive(true);
                right.SetActive(true);
                Time.timeScale = 1f;
                PlayerMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                isPaused = false;
            }
            if (PlayerMove == true)
            {


                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = -2f;
                }
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 move = transform.right * x + transform.forward * z;

                controller.Move(move * speed * Time.deltaTime);

                if (Input.GetButtonDown("Jump") && isGrounded)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);
            }
        }
    }
}