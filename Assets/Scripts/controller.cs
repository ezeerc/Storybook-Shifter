using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    #region Variables: Movement

    private Vector2 _input;
    private CharacterController _characterController;
    private Vector3 _direction2;

    [SerializeField] private float speed2 = 10;
    float rotatioSpeed = 100;

    #endregion
    #region Variables: Rotation

    [SerializeField] private float smoothTime2 = 0.05f;
    private float _currentVelocity2;

    #endregion
    #region Variables: Gravity

    private float _gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float _velocity;

    #endregion
    #region Variables: Jumping

    [SerializeField] private float jumpPower;

    #endregion

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _direction2 = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Rotate(0, _input.x * 100 * Time.deltaTime, 0);
        ApplyGravity();
        //ApplyRotation();
        ApplyMovement();
    }

    private void ApplyGravity()
    {
        if (IsGrounded2() && _velocity < 0.0f)
        {
            _velocity = -1.0f;
        }
        else
        {
            _velocity += _gravity * gravityMultiplier * Time.deltaTime;
        }

        _direction2.y = _velocity;
    }

    private void ApplyRotation()
    {
        //if (_input.sqrMagnitude == 0) return;

        //var targetAngle = Mathf.Atan2(_direction2.x, _direction2.z) * Mathf.Rad2Deg;
        //var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentVelocity2, smoothTime2);
        transform.Rotate(0, _input.x * 100* Time.deltaTime, 0);
    }
        private void ApplyMovement()
    {
        _characterController.Move(_direction2 * speed2 * Time.deltaTime);
    }

    public void Move()
    {
        //_input = context.ReadValue<Vector2>();
        _direction2 = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical") * Time.deltaTime);
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!IsGrounded2()) return;

        _velocity += jumpPower;
    }

    private bool IsGrounded2() => _characterController.isGrounded;
}