using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHp {get {return _maxHP;}}
    public float CurrentHp {get {return _currentHP;}}
    public float Armor {get {return _armor;}}
    public float MoveSpeed {get {return _moveSpeed;}}
    public float RunSpeed {get {return _runSpeed;}}
    public float TurnSpeed {get {return _turnSpeed;}}
    public float DashCount {get {return _dashCount;}}
    public float Stemina {get {return _jumpCount;}}
    public float JumpPower{get{return _jumpPower;}}
    public float DamageHeight{get{return _damageHeight;}}
    
    public bool IsRun {get {return _isRun;}set{_isRun = value;}}

    

    [SerializeField] protected float _maxHP;
    [SerializeField] protected float _currentHP;
    [SerializeField] protected float _armor;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _runSpeed;
    [SerializeField] protected float _turnSpeed;
    
    [SerializeField] protected float _dashCount;
    [SerializeField] protected float _stenina;
    [SerializeField] protected float _jumpCount;
    [SerializeField] protected float _jumpPower;
    [SerializeField] protected float _damageHeight;
    [SerializeField] protected float _attackCount;
    [SerializeField] protected bool _isRun;


    public void OnUpateState(float maxHp, float currentHp, float armor, float moveSpeed, float runspeed, float dashCount, float stemina)
    {
        this._maxHP = maxHp;
        this._currentHP = currentHp;
        this._armor = armor;
        this._moveSpeed = moveSpeed;
        this._runSpeed = runspeed;

        this._dashCount = dashCount;
        this._stenina = stemina;
    }
}
