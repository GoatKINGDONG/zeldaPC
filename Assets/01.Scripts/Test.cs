using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject test;
    [SerializeField] private float _count = 100;
    [SerializeField] private float _distance = 5;
    
    [SerializeField] private bool _isClick = false;
    [SerializeField] int _listCount;
    Player player => GetComponent<Player>();

    Rigidbody rb => GetComponent<Rigidbody>();  
    private void Start()
    {
        //_lineRenderer.enabled= false;
    }
    private void Update()
    {
       
    }
    Vector3 direction = Vector3.zero;
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();
        direction = new Vector3(input.x,input.y, input.z);
        LookAt();
    }

    protected void LookAt()
    {
        if(direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(direction);
            rb.rotation = targetAngle;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    protected void Move()
    {
        rb.velocity = direction * player.MoveSpeed * Time.fixedDeltaTime;
    }


   

    

}
