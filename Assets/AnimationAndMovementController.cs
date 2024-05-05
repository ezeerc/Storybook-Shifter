using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class AnimationAndMovementController : MonoBehaviour
{
    Controls controls;
    CharacterController characterController;
    Animator animator;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    float rotationFactorPerFrame = 5.0f;
    float runMultiplier = 3.0f;
    float moveSpeed = 5;
    float turnAmount = 5;
    float turnSpeed = 10;

    public static bool threeDimensions = true;
    Vector2 moveDirection;
    bool oneTime = false;

    Vector3 position2 = new Vector3(0, 0, 0);
    Vector3 position1 = new Vector3(0, 0, 0);
    bool euler;

    float desiredSpeed;
    float forwardSpeed;
    public float maxForwardSpeed = 8f;


    private void Awake()
    {
        controls = new Controls();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        /*controls.Player.Move.started += OnMovementInput;
        controls.Player.Move.canceled += OnMovementInput;
        controls.Player.Move.performed += OnMovementInput;
        controls.Player.Run.started += OnRun;
        controls.Player.Run.canceled += OnRun;*/
    }

    void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    /*void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }*/

    void Euler()
    {
        if (Input.GetKey(KeyCode.D))
        {
            euler = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            euler = false;
        }
    }
    /*void HandleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }

    }*/
    /*void HandleAnimation()
    {
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (isMovementPressed && !isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else if (!isMovementPressed && isWalking)
        {
            animator.SetBool("isWalking", false);
        }
    }*/

    // Update is called once per frame

    private void Move(Vector2 direction)
    {
        float turnAmount = direction.x;
        if (threeDimensions)
        {
            Vector3 direction2 = (direction.y * transform.forward) + (direction.x * transform.right);
        transform.position += (direction2 * moveSpeed * Time.deltaTime);

            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }

    }
    private void OnEnable()
    {
        controls.Enable();
    }

    void Tdimesions()
    {
        if (threeDimensions)
        {
            moveDirection = controls.Player.Move.ReadValue<Vector2>();
            if (!oneTime)
            {
                position2 = transform.position;
                transform.position = new Vector3(position1.x, position2.y, position2.z);
                oneTime = true;
            }
        }
        else
        {
            if (euler)
            {
                moveDirection = controls.Player.OnMove2d.ReadValue<Vector2>();
                transform.eulerAngles = new Vector3(0, 0, 0);
                if (oneTime)
                {
                    position1 = transform.position;
                    transform.position = new Vector3(0, position1.y, position1.z);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    oneTime = false;
                }
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
            if (!euler)
            {
                moveDirection = controls.Player.OnMove2d1.ReadValue<Vector2>();
                transform.eulerAngles = new Vector3(0, 180, 0);
                if (oneTime)
                {
                    position1 = transform.position;
                    transform.position = new Vector3(0, position1.y, position1.z);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    oneTime = false;
                }
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }

    }

    void Update()
    {
        isMovementPressed = moveDirection.x != 0 || moveDirection.y != 0;
        //HandleRotation();
        //onGround = IsGrounded();
        Tdimesions();
        Move(moveDirection);
        Euler();

        //HandleAnimation();
        /*       if (isRunPressed)
               {
                   characterController.Move(currentRunMovement * Time.deltaTime);
               }
               else
               {
                   Move(currentMovement);
               }*/
    }
}