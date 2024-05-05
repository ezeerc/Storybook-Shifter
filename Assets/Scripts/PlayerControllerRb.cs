using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerControllerRb : MonoBehaviour
{
    private Rigidbody rb;
    Animator anim;
    // Start is called before the first frame update
    Vector2 moveDirection;

    public static bool threeDimensions = true;
    bool oneTime = false;

    private Controls controls;

    Vector3 position2 = new Vector3(0, 0, 0);
    Vector3 position1 = new Vector3(0, 0, 0);
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
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (direction.sqrMagnitude > 1f)
            direction.Normalize();*/
    }
}
