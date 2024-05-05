using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
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
    public float jumpSpeed = 0.1f;
    public float groundRayDist = 1.1f;
    bool onGround;

    bool oneTime = false;
    bool readyJump = false;

    Vector3 position3 = new Vector3(0, 0, 0);
    Vector3 position2 = new Vector3(0, 0, 0);
    Vector3 position1 = new Vector3(0, 0, 0);

    const float groundAccel = 5;
    const float groundDecel = 25f;
    public float jumpForw;

    public static bool threeDimensions = true;

    Animator anim;
    Rigidbody rb;

    private Controls controls;

    bool IsMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpDirection = context.ReadValue<float>();
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        float rayLength = 0.1f;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
        {
            return true;
        }
        return false;
    }
    void Move(Vector2 direction)
    {
        float turnAmount = direction.x;
        float fDirection = direction.y;
        if (direction.sqrMagnitude > 1f)
            direction.Normalize();

        desiredSpeed = direction.magnitude * maxForwardSpeed; //* Mathf.Sign(fDirection);
        float acceleration = 16f;//IsMoveInput ? groundAccel : groundDecel;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredSpeed, acceleration * Time.deltaTime);
        anim.SetFloat("ForwardSpeed", forwardSpeed);

        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
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

        //rb.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
        anim.applyRootMotion = false;
        //float a = transform.forward.z *2;
        rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        //rb.AddForce(0, jumpSpeed, 0);
        rb.AddForce(this.transform.forward * 100 * 10);
        //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        anim.SetBool("Launch", false);
        onGround= false;

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
                position3.x = position1.x;
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
    bool euler = true;
    void Euler()
    {
        if (Input.GetKey(KeyCode.D)){
            euler = true;
        }
        else if (Input.GetKey(KeyCode.A)){
            euler = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!threeDimensions && collision.gameObject.tag == "platform")
        {
            position1.x = collision.gameObject.GetComponent<moveThreeD>().position3;
        }
        else if(!threeDimensions)
        {
            position1.x = position3.x;
        }
    }


    // Update is called once per frame
    void Update()
    {
        onGround = IsGrounded();
        Tdimesions();
        Move(moveDirection);
        Jump(jumpDirection);
        Euler();

        if (onGround)
        {
            anim.applyRootMotion = true;
            anim.SetBool("Land", true);
        }
        else
        {
            anim.SetBool("Land", false);
            anim.applyRootMotion = false;
        }
    }
}
