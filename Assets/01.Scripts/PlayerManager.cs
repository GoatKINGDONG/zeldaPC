using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, Character_Base
{
    [SerializeField] float Hp;
    [SerializeField] float Mp;


    

    public float HP(float damage)
    {
        return Hp + damage;
    }

    public float MP(float mp)
    {
        return Mp + mp;

    }

    
}
