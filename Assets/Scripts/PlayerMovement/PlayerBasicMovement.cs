using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicMovement : MonoBehaviour
{

    [SerializeField] private float _moveMaxSpeed;
    [SerializeField] private float _moveAccelerationSpeed;
    [SerializeField] private float _moveDecelerationSpeed;

    private Rigidbody2D _rb2d;
    private Vector2 _moveInput;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // MOVEMENT BASED ON FORCE
        /*if (_moveInput != Vector2.zero)
        {
            _rb2d.AddForce(_moveInput * _moveAccelerationSpeed);
            if (_rb2d.linearVelocity.magnitude > _moveMaxSpeed)
            {
                _rb2d.linearVelocity = _rb2d.linearVelocity.normalized * _moveMaxSpeed;
            }
        }
        else
        {
            _rb2d.AddForce(_rb2d.linearVelocity * -_moveDecelerationSpeed);
        }*/

        //MOVEMENT BASED ON VELOCITY

        _rb2d.linearVelocity = _moveInput * _moveMaxSpeed;


    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

}
