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
    private Animator _animator;
    private bool _isCanMove = true;


    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isCanMove)
        {
            _rb2d.linearVelocity = _moveInput * _moveMaxSpeed;
            _animator.SetFloat("Speed", _moveInput.magnitude);
        }
        else { _rb2d.linearVelocity = Vector2.zero; }
    }

    public void MoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void SetMoveAbilityOn() => _isCanMove = true;
    public void SetMoveAbilityOff() => _isCanMove = false;


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

}
