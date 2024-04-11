using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public E_Controller e_Controller;
    [SerializeField] int e_Controller_Index;
    [SerializeField] Transform _wand;
    [SerializeField] Transform _falm;
    private void FixedUpdate()
    {

    }
    private void Update()
    {
        Switch_E_Controller((int)e_Controller);
    }

    private void Start()
    {
        e_Controller = E_Controller.Normal;
    }
    void Switch_E_Controller(int e_Controller_Index)
    {
        switch (e_Controller_Index)
        {
            case 0:
                Debug.Log(e_Controller);
                _wand.gameObject.SetActive(false);
                break;
            case 1:
                Debug.Log(e_Controller);
                _wand.gameObject.SetActive(false);
                break;
            case 2:
                Debug.Log(e_Controller);
                _wand.gameObject.SetActive(true);
                _wand.transform.position = _falm.transform.position;
                _wand.transform.rotation = _falm.transform.rotation;
                break;
        }
    }
}
