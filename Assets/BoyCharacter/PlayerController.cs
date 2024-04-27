using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDirection;
    public float moveSpeed = 2f;
    public float maxForwardSpeed = 8f;
    float desiredSpeed;
    float forwardSpeed;
    float turnSpeed = 100;
    bool oneTime = false;

    Vector3 position2 = new Vector3(0, 0, 0);
    Vector3 position = new Vector3(0, 0, 0);

    const float groundAccel = 5;
    const float groundDecel = 25f;

    public static bool threeDimensions = true;

    Animator anim;

    private Controls controls;

    bool IsMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
    }


    /*public void OnMove3d(InputAction.CallbackContext context)
    {
        if (threeDimensions)
        {

            moveDirection = context.ReadValue<Vector2>();
        }
    }


    public void OnMove2d(InputAction.CallbackContext context)
    {
        if (!threeDimensions)
        {
            moveDirection = context.ReadValue<Vector2>();
        }

    }
    */
    void Move(Vector2 direction)
    {
        float turnAmount = direction.x;
        float fDirection = direction.y;
        if (direction.sqrMagnitude > 1f)
            direction.Normalize();

        desiredSpeed = direction.magnitude * maxForwardSpeed * Mathf.Sign(fDirection);
        float acceleration = IsMoveInput ? groundAccel : groundDecel;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredSpeed, acceleration * Time.deltaTime);
        anim.SetFloat("ForwardSpeed", forwardSpeed);

        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);

        //transform.Translate(direction.x * moveSpeed * Time.deltaTime, 0, direction.y * moveSpeed * Time.deltaTime);
    }

    void Awake()
    {
        controls = new();
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Tdimesions()
    {
        if (threeDimensions)
        {
            moveDirection = controls.Player.Move.ReadValue<Vector2>();
            if (!oneTime)
            {
                position2 = transform.position;
                transform.position = new Vector3(position.x, position2.y, position2.z);
                oneTime = true;
            }
        }
        else
        {
            moveDirection = controls.Player.OnMove2d.ReadValue<Vector2>();
            if (oneTime)
            {
                position = transform.position;
                transform.position = new Vector3(0, position.y, position.z);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                oneTime = false;
            }
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }



    // Update is called once per frame
    void Update()
    {
        Tdimesions();
        Move(moveDirection);
    }
}
