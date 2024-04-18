using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{

    [SerializeField] float _minDis = DrawMagic.instance._minDis;
    [SerializeField] Transform _magicStart = DrawMagic.instance._magicStart;
    LineRenderer lr;

    Vector3 prev;
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (lr.positionCount < 1)
            {
                Debug.Log("첫 클릭");
                prev = _magicStart.position;

                for (int i = 0; i < 2; i++)
                { SetLRPosition};
            }
        }

    }

    void UpdateLRPosition()
    {
        float _dis = Vector3.Distance(prev, transform.position);
        if (_dis > _minDis)
        {
            SetLRPosition();
            prev = transform.position;
        }

        if (_dis < _minDis && lr.positionCount > 0)
        {
            lr.SetPosition(lr.positionCount - 1, transform.position);
        }
    }


    void SetLRPosition()
    {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, transform.position);
    }
}
