using System;
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
public class PlayerControl : MonoBehaviour, I_PlayerBehavior
{
    [SerializeField] Transform Legs;
    public static PlayerControl instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public bool isGround = false;
    protected Player player;
    Rigidbody rb;
    Animator anim;


    public Vector3 direction { get; private set; }

    [SerializeField]
    [Tooltip("100이면 100퍼센트 움직임 - 애니메이션 속도도 영향을 줌")]
    protected float _moveSpeed = 100;
    //  속도를 나누기 위한 기준(100퍼센트)
    protected const float DEFAULT_SPEED = 100;

    private void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0f, input.y);
    }


    public void OnRunInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            player.IsRun = true;
        }
        else if (context.canceled)
        {
            player.IsRun = false;
        }
    }

    //  특정 상황이나 지형에 따른 속도를 변경하기 위한 함수 //  애니메이션 속도도 영향을 준다
    protected float ChangeSpeed()
    {
        return _moveSpeed / DEFAULT_SPEED;
    }

    protected void Move()
    {

        //  달리는지 체크용
        float runSpeed = player.IsRun == true ? player.RunSpeed : 1;

        //  실제 속도 : {정상 속도 * 달리기 속도} 움직이는 속도 * (디버프 혹은 지형에 따른 속도 변경)            
        float currentMoveSpeed = player.MoveSpeed * runSpeed * ChangeSpeed();
        LookAt();
        rb.velocity = direction * currentMoveSpeed + Vector3.up * rb.velocity.y;

        anim.SetFloat("A_Move", direction == Vector3.zero ? 0 : runSpeed);

        // I_MoveSpeed(GetAnimationSyncWithMovement(currentMoveSpeed));
        // Debug.Log(ChangeSpeed() + "바뀌는 속도");
        I_MoveSpeed(ChangeSpeed());
    }

    //  키보드 방향으로 방향 전환하기
    protected void LookAt()
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            rb.rotation = targetAngle;
        }
    }

    //  Jump 애니메이션에서 실행되는 것
    public void Jump()
    {
        rb.AddForce(Vector3.up * player.JumpPower, ForceMode.Impulse);
        anim.SetFloat("A_JumpMove", direction.magnitude);
    }

    //  버튼 클릭 시 점프가 동작 애니메이션
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetFloat("A_Move", 0f);
            anim.SetTrigger("A_Jump");
        }
    }

    void FixedUpdate()
    {
        Move();

        //  가속력이긴 함
        //  중력인데 
        //  이렇게 하면 공중에 떠 있는 시간만 따지게 되는 것과 같음
        yVelocity += gravity * Time.fixedDeltaTime;

    }
    private void OnDrawGizmos()
    {
        CheckGround(Legs);
    }
    RaycastHit hit;
    [SerializeField] float gravity;
    [SerializeField] float yVelocity;
    float maxValue = 0;
    float beforeValue = 0;
    private void Update()
    {
        // Debug.Log(yVelocity);

    }
    [SerializeField] bool isLandingSuccess = true;
    public void CheckGround(Transform Legs)
    {
        if (Physics.BoxCast(Legs.position, new Vector3(.5f, .5f, .5f) / 2, -Legs.up, out hit, Legs.rotation, float.MaxValue))
        {
            Debug.Log(hit.transform.gameObject.name);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(Legs.position, -Legs.up * hit.distance);
            Gizmos.DrawWireCube(hit.point, new Vector3(.5f, .5f, .5f));


            //  바닥에 닿아있다면   
            if (hit.distance < 0.1f)
            {
                if (maxValue > yVelocity)
                {
                    beforeValue = maxValue;
                    maxValue = yVelocity;
                }
                Debug.Log("isGround가 빠르다");
                //  높은 곳에서 떨어졌다면
                //  아마 지금 isLandingSuccess만 왔다갔다만 잘 되면 될 것 같음

                if (yVelocity < player.DamageHeight * -1 && isLandingSuccess == true)
                {
                    //  착지에 실패한 것
                    isLandingSuccess = false;
                    Debug.Log("데미지");
                    anim.SetBool("A_LandSuccess", isLandingSuccess);
                    // isLandingSuccess = true;

                }
                //  착지에 성공했다면
                else{
                    // isLandingSuccess = true;
                    Debug.Log("착지에 성공");
                }
                ResetYVelocity();
                isGround = true;
            }
            else
            {
                isGround = false;
            }
        }
    }
    void ResetYVelocity()
    {
        isLandingSuccess = true;
        yVelocity = 0;

    }
    public void I_MoveSpeed(float speed)
    {
        anim.speed = speed;
    }
}