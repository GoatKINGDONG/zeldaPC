using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAX : MonoBehaviour
{
    public int hp = 0;
    public int mp = 0;
    public string name;

    public void INFO () 
    {
        Debug.Log("MAX hp = " + hp + " mp = " + mp + " name = " + name);    
    }

    
}
