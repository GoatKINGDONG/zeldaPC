using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawMagic : MonoBehaviour
{
    [SerializeField] float _distance = 1;
    [SerializeField] private GameObject wand_MagicSpot;
    LineRenderer lr;
    [SerializeField] private Vector3 prevPos = Vector3.zero;
    [SerializeField] float _dis;

    [SerializeField] Material _magicMat;
    Material tmp_Magic;
    
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        //lr.positionCount= 0;
        prevPos = wand_MagicSpot.transform.position;

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Draw_Magic();
        }

        else
        {
            lr.positionCount = 0;
        }
    }

    private void Draw_Magic()
    {
        _dis = Vector3.Distance(prevPos, wand_MagicSpot.transform.position);
        if(Vector3.Distance(prevPos, wand_MagicSpot.transform.position) > _distance)
        {
            lr.positionCount++;
            lr.SetPosition(lr.positionCount-1, wand_MagicSpot.transform.position);
            prevPos = wand_MagicSpot.transform.position;
        }
        else
        {
            if (lr.positionCount > 0)
            {
                lr.SetPosition(lr.positionCount - 1, wand_MagicSpot.transform.position);
            }
        }
    }







}
