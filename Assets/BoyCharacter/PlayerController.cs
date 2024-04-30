using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDirection;
    float jumpDirection;
    public float moveSpeed = 2f;
    public float maxForwardSpeed = 8f;
    float desiredSpeed;
    float forwardSpeed;
    float turnSpeed = 100;
    public float jumpSpeed = 20000f;
    public float groundRayDist = 1f;

    bool oneTime = false;
    bool readyJump = false;
    bool onGround = true;

    Vector3 position2 = new Vector3(0, 0, 0);
    Vector3 position1 = new Vector3(0, 0, 0);

    const float groundAccel = 5;
    const float groundDecel = 25f;

    public static bool threeDimensions = true;

    Animator anim;
    Rigidbody rb;

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

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpDirection = context.ReadValue<float>();
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

    void Jump(float jumpDirection)
    {
        if (jumpDirection > 0 && onGround)
        {
            anim.SetBool("ReadyJump", true);
            readyJump = true;
        }
        else if (readyJump)
        {
            anim.SetBool("Launch", true);
            readyJump = false;
            anim.SetBool("ReadyJump", false);
        }
    }

    public void Launch()
    {
        rb.AddForce(0, jumpSpeed, 0);
        anim.SetBool("Launch", false);
        anim.applyRootMotion = false;
        onGround = false;
    }

    public void Land()
    {
        anim.SetBool("Land", false);
        anim.applyRootMotion = true;
        anim.SetBool("Launch", false);
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
        rb = this.GetComponent<Rigidbody>();
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
            moveDirection = controls.Player.OnMove2d.ReadValue<Vector2>();
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



    // Update is called once per frame
    void Update()
    {
        Tdimesions();
        Move(moveDirection);
        Jump(jumpDirection);

        RaycastHit hit;
        Ray ray = new Ray(this.transform.position + Vector3.up * groundRayDist * 0.5f, -Vector3.up);
        if (Physics.Raycast(ray, out hit, groundRayDist))
        {
            if (!onGround)
            {
                onGround = true;
                anim.SetBool("Land", true);
                Debug.Log("estoy en tierra");
            }
        }
        else
        {
            onGround = false;
            anim.applyRootMotion = false;
        }

        Debug.DrawRay(this.transform.position + Vector3.up * groundRayDist * 0.5f, - Vector3.up * groundRayDist, Color.red);
    }
}
