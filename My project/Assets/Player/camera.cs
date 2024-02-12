using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class camera : MonoBehaviour
{
    private PlayerInput controls;
    private float mouseSensitivity = 100f;
    private Vector2 mouseLook;
    private float xRotation = 0f;
    public Transform playerBody;
    [SerializeField] private float MouseSens;

    void Awake()
    {
        controls = new PlayerInput();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
       controls.Disable();
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        mouseLook = controls.Player.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * mouseSensitivity * MouseSens * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSensitivity * MouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f,90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
