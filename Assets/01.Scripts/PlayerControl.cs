using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

[RequireComponent(typeof(Player))]
public class PlayerControl : MonoBehaviour ,I_PlayerBehavior
{
  
    // private CheckFloor _checkFloor;
    // public Transform LegsCheck;

    // public static Collider[] floorCollider;
    // private Rigidbody _rb;
    // [SerializeField] private float _speed;
    // [SerializeField] private float _gravity;
    // [SerializeField] private float _jumpPower = 5f;
    // [SerializeField] private float _jumpTime = 2;
    // [SerializeField] private float _jumpDamage_Max = -1.9f;
    // [SerializeField] private float _jumpDamage_Min = -0.1f;
    // [SerializeField] private bool _isJump;
    // [SerializeField] private float _runSpeed = 1;
    // [SerializeField] private float _turnSpeed = 20;

    // [SerializeField] private int _attackTime = 0;
    // [SerializeField] private int _max_AttackTime = 3;

    // //  부정확한 바닥체크 다시 확인하기 위하여
    // [SerializeField] private float _maxDistance;


    // [SerializeField] public float _yVelocity = 0;
    // [SerializeField] public float _landingVelociry = 0;
    // [SerializeField] public bool isFloor;
    // private Vector2 _moveDirection;

    // [SerializeField] private bool _isMove;
    // private Vector3 _input;
    // private Vector3 _lookDir;
    // private Animator _anim;




    // PlayerInputSystem _playerInputSystem;


    // private void Awake()
    // {
    //     _checkFloor = LegsCheck.GetComponent<CheckFloor>();
    //     _rb = GetComponent<Rigidbody>();
    //     _playerInputSystem = new PlayerInputSystem();
    //     _anim = GetComponent<Animator>();

    // }

    // /// <summary>
    // /// This function is called when the object becomes enabled and active.
    // /// </summary>
    // private void OnEnable()
    // {
    //     _playerInputSystem.Enable();

    // }

    // private void FixedUpdate()
    // {
    //     Gravity();
    // }

    // private void Gravity()
    // {
    //     if (isFloor == true)    //  바닥에 닿았을 때
    //     {
    //         if (_yVelocity <= _jumpDamage_Max) //  가속력이 몇 이하라면
    //         {
    //             Debug.Log("낙뎀!");
    //         }
    //         else if (_yVelocity >= _jumpDamage_Max && _yVelocity <= _jumpDamage_Min) //  가속력이 적당하다면
    //         {
    //             Debug.Log("착지 성공");
    //         }
    //         _anim.SetFloat("A_Land", _yVelocity);
    //         _yVelocity = 0;
    //     }
    //     else
    //     {
    //         _yVelocity += _gravity * Time.fixedDeltaTime;
    //     }

    //     Move();
    // }
    // private void OnDisable()
    // {
    //     _playerInputSystem.Disable();
    // }

    // public void OnMove(InputAction.CallbackContext context)
    // {
    //     _input = context.ReadValue<Vector2>();

    // }
    // private void Move()
    // {
    //     _moveDirection = _input * _speed * _runSpeed * Time.fixedDeltaTime;
    //     if (_isMove)
    //     {
    //         _lookDir = new Vector3(_input.x, 0, _input.y);
    //         if (_moveDirection.sqrMagnitude != 0)   //  움직이는 상태라면
    //         {
    //             transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_lookDir), _turnSpeed * Time.fixedDeltaTime);
    //         }

    //         _rb.velocity = new Vector3(_moveDirection.x, _rb.velocity.y, _moveDirection.y);
    //         _anim.SetFloat("A_Move", _moveDirection.magnitude * _speed * _runSpeed);
    //         Debug.Log(_moveDirection.magnitude * _speed * _runSpeed);
    //     }
    // }

    // public void RealJump()
    // {
    //     _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);

    // }

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.tag == "FLOOR")
    //     {
    //         if (_isJump == false)
    //         {
    //             _isJump = true;
    //         }
    //     }
    // }

    // public void OnJump(InputAction.CallbackContext context)
    // {
    //     if (context.performed && _isMove == true)
    //     {

    //         _anim.SetTrigger("A_Jump_T");
    //         _isJump = false;
    //     }
    // }

    // public void OnRun(InputAction.CallbackContext context)
    // {
    //     if (context.performed) { _runSpeed = 2; }
    //     if (context.canceled) { _runSpeed = 1; }
    // }

    // public void StandUp(float success)
    // {
    //     if (success == 0)
    //     {
    //         _isMove = false;
    //     }
    //     else
    //     {
    //         _isMove = true;
    //     }
    // }

    // public void OnAttack(InputAction.CallbackContext context)
    // {
    //     Debug.Log("wow");
    //     if(context.performed)
    //     {
    //         Debug.Log("마우스 클릭함");
    //         if(context.interaction is HoldInteraction)  //  차지 공격
    //         {
    //             Debug.Log("스킬사용");
    //         }
    //         else if(context.interaction is PressInteraction)    //  일반 공격
    //         {
    //             Debug.Log("일반 공격");               
    //         }
    //     }
    // }
    // public void I_Attack(float damage)
    // {   
    //     _attackTime++;

    //     if(_attackTime <_max_AttackTime)
    //     {
    //         _anim.SetTrigger("A_Attack");            
    //     }
    //     else
    //     {
    //         _attackTime = 0;
    //     }
    // }

    // public void I_Damage(float damage)
    // {
    //     throw new System.NotImplementedException();
    // }

    // public void I_Die()
    // {
    //     throw new System.NotImplementedException();
    // }

    // public void I_Shield(float damage)
    // {
    //     throw new System.NotImplementedException();
    // }

    public enum PlayerMoveEffect{
        NORMAL =1,
        FREEZE = 7,
        FAST = 2,
        SWIMMING = 8
    }
    protected PlayerMoveEffect e_PlayerMoveEffect;

      public enum PlayerState{
        MOVE,   //  평시 상태(idle, run, runfast
        JUMP
    }

    protected PlayerState e_PlayerState;
    private Coroutine JumpCoroutine;

    private IEnumerator JumpCoroutine_C(){
       
            Debug.Log("점프");
            rb.AddForce(Vector3.up * player.JumpPower, ForceMode.Impulse);
            anim.SetFloat("A_Move",0f);
            anim.SetTrigger("A_Jump");
            // anim.SetBool("A_IsJump",true);
            // anim.SetFloat("A_Move",0);
            yield return 0;
    }

    Rigidbody rb;
    Animator anim;
    protected Player player;
    
    [SerializeField] protected float _moveSpeed = 100; //  100이면 100퍼센트의 움직임
    
    protected const float DEFAULT_SPEED = 100;
    
    private void Start() 
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public Vector3 direction {get; private set;}

    public void OnMoveInput(InputAction.CallbackContext context){
        Vector2 input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0f, input.y);
    }

   
    public void OnRunInput(InputAction.CallbackContext context){
        if(context.performed){
            player.IsRun = true;
        }
        else if(context.canceled){
            player.IsRun = false;
        }
    }

    //  특정 상황이나 지형에 따른 속도를 변경하기 위한 함수
    protected float ChangeSpeed()
    {
        //  현재 움직인다면?
        if(direction != Vector3.zero)
        {
            //  움직이는 속도 나누기 100임
            return _moveSpeed / DEFAULT_SPEED;
        }
        //  움직이지 않는다면?  
        //  버그 -> 여기서 발생하는 것 같음
        else{
            return 0;
        }
    }

    //  애니메이션과 움직임의 속도를 맞추기 위함
    protected float GetAnimationSyncWithMovement(float currentMoveSpeed)
    {
        //  만약 움직이는 키를 눌렀다면?
        if(direction != Vector3.zero)
        {
            //  1과 2로 나누기 위한 것 현재 움직이는 속도 / 움직이는 속도
            //  현재 움직이는 속도 = 정상 속도 * 달리기 속도 * 디버프 속도(디버프에서 속도를 제어함)
            //  
            return currentMoveSpeed/player.MoveSpeed;            
        }     
        else return 1;    
    }
 

    protected void Move(){

            //  달리는지 체크용
            float runSpeed = player.IsRun == true? player.RunSpeed:1;
            
            //  실제 속도 : 정상 속도 * 달리기 속도 * (디버프 혹은 지형에 따른 속도 변경)            
            float currentMoveSpeed = player.MoveSpeed * runSpeed * ChangeSpeed();                    
            LookAt();
            rb.velocity = direction * currentMoveSpeed + Vector3.up * rb.velocity.y;
            
            anim.SetFloat("A_Move",direction == Vector3.zero ? 0:runSpeed);
            
            I_MoveSpeed(GetAnimationSyncWithMovement(currentMoveSpeed));
    }

    protected void LookAt(){
        if(direction != Vector3.zero){
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            rb.rotation = targetAngle;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.performed){
            StartCoroutine(JumpCoroutine_C());
        }
    }
    
    void FixedUpdate(){
        Move();
    }

    public void I_MoveSpeed(float speed)
    {
        anim.speed = speed;
    }
}