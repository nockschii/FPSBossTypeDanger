using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Transform playerBody;

    private float xAxisClamp;

	// Use this for initialization
	void Start ()
    {
        LockCursor();
        xAxisClamp = .0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        CameraRotation();
	}
    
    // Lock cursor to the center of the screen
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Rotate camera by using mouse input
    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        // keeping a constant record of rotation of our camera
        xAxisClamp += mouseY;

        if(xAxisClamp > 90.0f) // looking directly upward
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationValue(270.0f);
        }
        else if (xAxisClamp < -90.0f) // looking directly upward
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationValue(90.0f);
        }

        // Rotate camera by passing vector3
        transform.Rotate(Vector3.left * mouseY);

        // Access player body transform
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Stops the camera rotation exceeding the clamp
    private void ClampXAxisRotationValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
