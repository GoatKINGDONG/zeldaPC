using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ST_STUDENT 
{
    
    public int hp;
    public int mp;
    public string name;

    public void INFO () 
    {
        Debug.Log("MAX hp = " + hp + " mp = " + mp + " name = " + name);    
    }
}
