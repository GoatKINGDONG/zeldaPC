using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(LineRenderer))]
public class OnClick : MonoBehaviour, I_Magic
{
   
    E_Controller e_Controller;
    [SerializeField]
    Vector3 previousPos;
    [SerializeField] Transform magic_Start;

    [SerializeField]
    float _drawDistance = 1;
    [SerializeField]
    float _dis;
    [SerializeField]
    float _sLerp;
  
    

    [SerializeField] Transform _wand;

    LineRenderer lr => GetComponent<LineRenderer>();

    public void I_Click()
    {

    }
    private void Start()
    {
        previousPos = transform.position;
    }
    public void I_Magic()
    {
        if (Input.GetMouseButton(0))
        {
            _dis = Vector3.Distance(previousPos, magic_Start.position);
            if (Vector3.Distance(previousPos, magic_Start.position) > _drawDistance)
            {
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, magic_Start.position);
                //lr.SetPosition(lr.positionCount - 1, Vector3.Slerp(previousPos, transform.position, _sLerp));
                previousPos = transform.position;
            }
            else if (Vector3.Distance(previousPos, magic_Start.position) <= _drawDistance && lr.positionCount > 0)
            {
                lr.SetPosition(lr.positionCount - 1, magic_Start.position);
            }
            //previousPos = transform.position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lr.positionCount = 0;
            Debug.Log("좌클릭 버튼 취소");
        }
    }
    
    [SerializeField] Transform player;
    
    void Update()
    {

        //Change(test.e_Controller);
        //if (e_Click_State.Magic == click_State)
        //{
        //    I_Magic();
        //}
        //switch (click_State)
        //{
        //    case e_Click_State.Normal:
        //        lr.enabled = false;
        //        break;
        //    case e_Click_State.Grab:
        //        lr.enabled = false;
        //        break;
        //    case e_Click_State.Magic:
        //        lr.enabled = transform;
        //        break;
        //}

    }

    [SerializeField] protected int controller_numb;

    void Change(E_Controller e_Controller)
    {
        if (controller_numb >= 0 && controller_numb <= 3)
        {
            //ChangeClickState(controller_numb);
            switch (e_Controller)
            {
                case E_Controller.Normal:
                    _wand.gameObject.SetActive(false);
                    break;
                case E_Controller.Grab:
                    _wand.gameObject.SetActive(false);
                    break;
                case E_Controller.Magic:
                    _wand.gameObject.SetActive(true); break;

            }
        }
        else
        {
            return;
        }
    }
    
  


}
