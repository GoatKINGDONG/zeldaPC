using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerControl : MonoBehaviour
{

    private CheckFloor _checkFloor;
    public Transform LegsCheck;

    public static Collider[] floorCollider;
    private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _jumpTime = 2;
    [SerializeField] private float _jumpDamage_Max = -1.9f;
    [SerializeField] private float _jumpDamage_Min = -0.1f;
    [SerializeField] private bool _isJump;
    [SerializeField] private float _runSpeed = 1;
    [SerializeField] private float _turnSpeed = 20;

    //  부정확한 바닥체크 다시 확인하기 위하여
    [SerializeField] private float _maxDistance;


    [SerializeField] public float _yVelocity = 0;
    [SerializeField] public float _landingVelociry = 0;
    [SerializeField] public bool isFloor;
    private Vector2 _moveDirection;

    [SerializeField] private bool _isMove;
    private Vector3 _input;
    private Vector3 _lookDir;
    private Animator _anim;




    PlayerInputSystem _playerInputSystem;


    private void Awake()
    {
        _checkFloor = LegsCheck.GetComponent<CheckFloor>();
        _rb = GetComponent<Rigidbody>();
        _playerInputSystem = new PlayerInputSystem();
        _anim = GetComponent<Animator>();

    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        _playerInputSystem.Enable();

    }

    private void FixedUpdate()
    {
        Gravity();
        Move();
    }

    private void Gravity()
    {
        if (isFloor == true)    //  바닥에 닿았을 때
        {
            if (_yVelocity <= _jumpDamage_Max) //  가속력이 몇 이하라면
            {
                Debug.Log("낙뎀!");
            }
            else if (_yVelocity >= _jumpDamage_Max && _yVelocity <= _jumpDamage_Min) //  가속력이 적당하다면
            {
                Debug.Log("착지 성공");
            }
            _anim.SetFloat("A_Land", _yVelocity);
            _yVelocity = 0;
        }
        else
        {
            _yVelocity += _gravity * Time.fixedDeltaTime;
        }
    }
    private void OnDisable()
    {
        _playerInputSystem.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();

    }
    private void Move()
    {
        _moveDirection = _input * _speed * _runSpeed * Time.fixedDeltaTime;
        if (_isMove)
        {
            _lookDir = new Vector3(_input.x, 0, _input.y);
            if (_moveDirection.sqrMagnitude != 0)   //  움직이는 상태라면
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_lookDir), _turnSpeed * Time.fixedDeltaTime);
            }

            _rb.velocity = new Vector3(_moveDirection.x, _rb.velocity.y, _moveDirection.y);
            _anim.SetFloat("A_Move", _moveDirection.magnitude * _speed * _runSpeed);
            Debug.Log(_moveDirection.magnitude * _speed * _runSpeed);
        }
    }

    public void RealJump()
    {
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "FLOOR")
        {
            if (_isJump == false)
            {
                _isJump = true;
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && _isMove == true)
        {

            _anim.SetTrigger("A_Jump_T");
            _isJump = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed) { _runSpeed = 2; }
        if (context.canceled) { _runSpeed = 1; }
    }

    public void StandUp(float success)
    {
        if (success == 0)
        {
            _isMove = false;
        }
        else
        {
            _isMove = true;
        }
    }
}