using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDHMAX : MonoBehaviour
{
    
    ST_STUDENT monter1;
    ST_STUDENT monter2;

    MAX bose1;
    MAX bose2;

    private void Start() {
        monter1 = new ST_STUDENT();
        monter2 = monter1;
        monter1.name = "monster1";
        monter1.hp = 100;
        monter1.mp = 50;

        monter2.hp = 200;
        monter2.mp = 150;
        monter2.name = "monster2";

        bose1 = new MAX();
        bose2 = bose1;

        bose1.name = "bose1";
        bose1.hp = 1000;
        bose1.mp= 1500;

        bose2.name = "bose2222222";
        bose2.hp = 1000000;
        bose2.mp = 4909999;

        
    }

    private void Update() {
        monter1.INFO();
        monter2.INFO();

        bose1.INFO();
        bose2.INFO();
    }
}
