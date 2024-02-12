using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PlayerInput controls;
    [SerializeField] private float moveSpeed = 12f;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private Vector2 move;
    private float jumpHeight = 2.4f;
    private Vector3 effectiveDirection = Vector3.zero;
    private CharacterController controller;
    public Transform ground;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;
    [SerializeField] private bool isGrounded = true;
    private Rigidbody PlayerRigidbody;
    float currentspeed;
    [SerializeField] Rigidbody rb;
    [SerializeField] public float directionSmooth = 0.5f;
    public Vector3 movement;
    [SerializeField] AudioSource footstepSound;
    [SerializeField] AudioClip jump;



    private void Awake()
    {
        controls = new PlayerInput();
        controller = GetComponent<CharacterController>();
        PlayerRigidbody = GetComponent<Rigidbody>();

        rb = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        Grav();
        PlayerMovement();
        Jump();

        if (Input.GetKeyDown(KeyCode.R))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (isGrounded)
            {
                footstepSound.enabled = true;
            }
            else
            {
                footstepSound.enabled = false;
            }
        }
        else
        {
            footstepSound.enabled = false;
        }

    }

    private void Grav()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);
        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
            
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
    }
    private void PlayerMovement()
    {
       move = controls.Player.Movement.ReadValue<Vector2>();

       Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
       controller.Move(effectiveDirection * moveSpeed * Time.deltaTime);
        effectiveDirection = Vector3.Lerp(effectiveDirection, movement, directionSmooth);
       
    }

    private void Jump()
    {
        if (controls.Player.Jump.triggered && isGrounded)
        { 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isGrounded = true;
            AudioSource.PlayClipAtPoint(jump, transform.position);


        }
    }
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;

    }

}
