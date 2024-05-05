using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerControllerV : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController controller;
    Vector2 movement;
    public float moveSpeed;

    public static bool threeDimensions = true;
    bool oneTime = false;

    private Controls controls;

    Vector3 position2 = new Vector3(0, 0, 0);
    Vector3 position1 = new Vector3(0, 0, 0);
    void Tdimesions()
    {
        if (threeDimensions)
        {
            movement = controls.Player.Move.ReadValue<Vector2>();
            if (!oneTime)
            {
                position2 = transform.position;
                transform.position = new Vector3(position1.x, position2.y, position2.z);
                oneTime = true;
            }
        }
        else
        {
            movement = controls.Player.OnMove2d.ReadValue<Vector2>();
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
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        Tdimesions();

        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
}
