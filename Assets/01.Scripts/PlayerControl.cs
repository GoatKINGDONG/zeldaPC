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
public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;
    Player player;
    Vector3 _direction = Vector3.zero;
    private void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Update()
    {
        LookAt();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();
        _direction = input;
    }

    public void Move()
    {
        rb.velocity = _direction * Time.fixedDeltaTime * player.MoveSpeed;
    }

    protected void LookAt() 
    {
        if (_direction != Vector3.zero)
        {
            Quaternion targetAngle = Quaternion.LookRotation(_direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetAngle, player.TurnSpeed);      
        }

    }
}