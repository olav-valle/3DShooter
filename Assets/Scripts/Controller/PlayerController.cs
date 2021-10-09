using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the movement of the player with given input from the input manager
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Settings")] [Tooltip("The speed at which the player moves")]
    public float moveSpeed = 2f;

    [Tooltip("The speed at which the player rotates to look left and right (calculated in degrees)")]
    public float lookSpeed = 60f;

    [Tooltip("The power that the player jumps with")]
    public float jumpPower = 8f;

    [Tooltip("The force of gravity acting on the player")]
    public float gravity = 9.81f;

    [Header("Required references")] [Tooltip("The player shooter script that fires projectiles")]
    public Shooter playerShooter;


    // Additional components of the player component.
    CharacterController controller;
    InputManager inputManager;




    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Start()
    {
        SetUpCharacterController();
        SetUpInputManager();
    }

    /// <summary>
    /// Set up CharacterController object for this object.
    /// </summary>
    void SetUpCharacterController()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("The player controller script does not have a CharacterController on the same object.");
        }
    }

    void SetUpInputManager()
    {
        inputManager = InputManager.instance;
        if (inputManager == null)
        {
            Debug.LogError("The player controller script does not have an InputManager on the same object.");
        }
    }


    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Update()
    {
        ProcessMovement();
    }

    // movement global
    Vector3 moveDirection;
    
    void ProcessMovement()
    {

        float leftRightInput = inputManager.horizontalMoveAxis;
        float forwardBackwardInput = inputManager.verticalMoveAxis;
        bool jumpPressed = inputManager.jumpPressed;
        
        
        // Handle the control of the player while it's on the ground.
        if (controller.isGrounded)
        {
            // stores xyz movement data from inputManager. set y=0, since we are on the ground.
            moveDirection = new Vector3(leftRightInput, 0, forwardBackwardInput);
            // set the move direction in relation to the transform.
            moveDirection = transform.TransformDirection(moveDirection);

            moveDirection = moveDirection * moveSpeed;

            // Should we jump?
            if (jumpPressed)
            {
                moveDirection.y = jumpPower;
            }

        }

        // applies gravity to player jump movement.
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);


        // Debug.Log("The horizontal input is: " + leftRightInput);


    }
}
