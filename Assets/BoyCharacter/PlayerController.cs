using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    Vector2 moveDirection;
    public float moveSpeed = 2f;
    public float maxForwardSpeed = 8f;
    float desiredSpeed;
    float forwardSpeed;
    float turnSpeed = 100;

    const float groundAccel = 5;
    const float groundDecel = 25f;

    public bool threeDimensions = false;

    Animator anim;

    bool IsMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
    }



    public void OnMove3d(InputAction.CallbackContext context)
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
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move(moveDirection);
    }
}
