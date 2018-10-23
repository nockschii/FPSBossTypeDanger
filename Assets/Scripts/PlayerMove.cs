using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName; // A + D keys
    [SerializeField] private string verticalInputName; // W + S keys
    [SerializeField] private float movementSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float runBuildUp;
    [SerializeField] private KeyCode runKey;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private KeyCode crouchKey;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] public AudioSource BackgroundMusic;

    private CharacterController charController;
    private float targetHeight = 0.5f;
    private float runningTime = 2f;
    private bool isCrouching;
    private bool isJumping;
    private bool isRunning;
    public static bool GameIsPaused = true;

    // Use this for initialization
    void Start()
    {
        charController = GetComponent<CharacterController>();
        BackgroundMusic.Play();
        BackgroundMusic.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            GameIsPaused = false;
            playerMovement();
        }
        else
        {
            GameIsPaused = true; 
        }
        
        handleMusic(GameIsPaused);
    }

    private void handleMusic(bool gameIsPaused)
    {
        if (gameIsPaused == false)
        {
            BackgroundMusic.UnPause();
        }
        else
        {
            BackgroundMusic.Pause();
        }
        Debug.Log("TIME: "+BackgroundMusic.time+"GamePause: "+GameIsPaused+"GamePlayed: "+gameIsPaused);
    }

    private void playerMovement()
    {
        // input WASD
        float verticalInput = Input.GetAxis(verticalInputName);
        float horizontalInput = Input.GetAxis(horizontalInputName);

        // movement variables
        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 rightMovement = transform.right * horizontalInput;

        // Controller moving according to variables // SimpleMove applies Time.deltaTime by default
        // ClampMagnitude ensures that moving diagonally is limited => so it's not faster than moving normally
        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);

        // if we are moving on a slope we want to apply slopeForce
        if ((verticalInput != 0 || horizontalInput != 0) && onSlope())
        {
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);
        }

        crouchInput();
        jumpInput();
        setMovementSpeed();
    }

    private void crouchInput()
    {
        if (Input.GetKey(KeyCode.C))
        {
            isCrouching = true;
            targetHeight = 0.5f;
        }
        else
        {
            isCrouching = false;
            charController.height = 2.0f;
        }
        charController.height = Mathf.Lerp(charController.height, targetHeight, 5f * Time.deltaTime);
    }

    // handles jump input
    private void jumpInput()
    {
        // jumpKey pressed and player is not jumping
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(jumpEvent());
        }
    }

    // returns IEnumerator because of Coroutine
    private IEnumerator jumpEvent()
    {
        charController.slopeLimit = 90.0f;

        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;

            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;

        isJumping = false;
    }

    // charController is on slope
    private bool onSlope()
    {
        if (isJumping) return false;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up) return true;
        }

        return false;
    }

    private void setMovementSpeed()
    {
        if (Input.GetKey(runKey))
        {
            isRunning = true;
            //Debug.Log(isRunning);
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUp);
        }
        else
        {
            isRunning = false;
            //Debug.Log(isRunning);
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUp);
        }
    }

}
