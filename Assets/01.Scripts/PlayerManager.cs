using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public E_Controller e_Controller;
    [SerializeField] int e_Controller_Index;
    [SerializeField] float wheel;
    [SerializeField] float wheel2;
    
    [SerializeField] Transform _wand;
    [SerializeField] Transform _falm;

    private void FixedUpdate()
    {

    }
    private void Update()
    {

        Wheel();
        Switch_E_Controller(e_Controller);
    }

    private void Start()
    {
        e_Controller = E_Controller.Normal;
    }
    void Switch_E_Controller(E_Controller e)
    {
        switch ((int)e_Controller)
        {
            case 0:
                
                _wand.gameObject.SetActive(false);
                break;
            case 1:
                
                _wand.gameObject.SetActive(false);
                break;
            case 2:
                
                _wand.gameObject.SetActive(true);
                _wand.transform.position = _falm.transform.position;
                _wand.transform.rotation = _falm.transform.rotation;
                break;
        }
    }
    void Wheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (wheel == 2)
            {
                return;
            }
            else
            {
                ++wheel;
                e_Controller++;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (wheel == 0)
            {
                return;
            }
            else
            {
                --wheel;
                e_Controller--;
            }

        }
       


    }
}
