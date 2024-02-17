using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public static Collider[] floorCollider;
    private CharacterController _cc;
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpPower = 5f;
    [SerializeField] private float _jumpTime = 2;
    [SerializeField] private float _runSpeed = 1;

    //  부정확한 바닥체크 다시 확인하기 위하여
    [SerializeField] private float _maxDistance;
    [SerializeField] private Vector3 _boxSize;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool drawGizmo;

    [SerializeField] private float _yVelocity = 0;
    private Vector3 _dir;

    private Vector2 _in_dir;
    private bool _jump = true;  //  점프를 할 수 있는 상태
    private bool _isGround = true;  //  바닥에 닿아있는 가?
    private Animator _anim;




    PlayerInputSystem _playerInputSystem;

    InputAction _run_Action;


    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _playerInputSystem = new PlayerInputSystem();
        _run_Action = _playerInputSystem.Player.Run;
        _anim = GetComponent<Animator>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        _playerInputSystem.Enable();
        _run_Action.started += ORun;
        _run_Action.canceled += OnRunRelease;
    }

    private void OnDisable()
    {
        _playerInputSystem.Disable();
        _run_Action.started -= ORun;
        _run_Action.canceled -= OnRunRelease;
    }

    void Start()
    {
        _cc = GetComponent<CharacterController>();
    }




    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        _dir = new Vector3(_in_dir.x * _speed * _runSpeed * Time.fixedDeltaTime, 0, _in_dir.y * _speed * _runSpeed * Time.fixedDeltaTime);
        Gravity();
        // _yVelocity += _gravity * Time.deltaTime;
        // _dir.y = _yVelocity;

        _cc.Move(_dir);
        if (_in_dir.magnitude != 0)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(_in_dir.x, 0, _in_dir.y)), Time.fixedDeltaTime * 30);
        }
        _anim.SetFloat("A_Move", _in_dir.magnitude * _speed * _runSpeed);

    }
    public void OnMove(InputValue value)
    {
        if (value != null)
        {
            _in_dir = value.Get<Vector2>();
        }
    }
    void Gravity()
    {
        _yVelocity += _gravity * Time.fixedDeltaTime;
        _dir.y = _yVelocity;
    }

    public void RealJump()
    {
        _yVelocity = _jumpPower;
    }

    public void OnJump()
    {
        Debug.Log("점프!");
        _anim.SetBool("A_Jump", _jump);
        // _jump = false;
        // _yVelocity = _jumpPower;
    }

    public void ORun(InputAction.CallbackContext context)
    {
        _runSpeed = 2;
        Debug.Log("달리기 시작");
    }

    public void OnRunRelease(InputAction.CallbackContext context)
    {
        Debug.Log("달리기 종료");
        _runSpeed = 1;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.rigidbody)
        {
            hit.rigidbody.AddForce(_dir/hit.rigidbody.mass);
        }
    }
}
