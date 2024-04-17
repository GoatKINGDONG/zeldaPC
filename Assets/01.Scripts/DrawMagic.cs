using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    [SerializeField] float _time = 0;

    [SerializeField] Material _magicMat;
    [SerializeField] GameObject magic;
    [SerializeField] List<GameObject> magicList;
    //Material tmp_Magic;

    private void Start()
    {
        _time = 0;
        //lr = GetComponent<LineRenderer>();
        lr= magic.GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        
        prevPos = wand_MagicSpot.transform.position;

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lr.material = _magicMat;
            magicList.Add(magic);
            magicList[0].SetActive(true);
        }
        else if (Input.GetMouseButton(0))
        {
            Draw_Magic();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(MagicEnd(10));

        }
    }
    

    
    private void Draw_Magic()   
    {
        //lr.material = _magicMat;

        _dis = Vector3.Distance(prevPos, wand_MagicSpot.transform.position);
        if (Vector3.Distance(prevPos, wand_MagicSpot.transform.position) > _distance)
        {
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, wand_MagicSpot.transform.position);
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
    
    
    
    //  ���� ����� ���� �� õõ�� �������(MaxTime�� ���缭)
    IEnumerator MagicEnd(float MaxTime)
    {
        
        Debug.Log("�ڷ�ƾ ����");
        float timer = 0;
        Color mat_Color = lr.material.color;
        Color tmp = lr.material.color;
        
        float maxFixedTime = MaxTime / Time.fixedDeltaTime; //  �����Ӱ����� ��������
        

        while (timer < MaxTime)
        {
            
            timer += Time.fixedDeltaTime;
            //tmp2 = timer;
            float alpha = Mathf.Lerp(mat_Color.a, 0, timer / maxFixedTime);
            
            mat_Color.a = alpha;
            
            
            lr.material.color = mat_Color;
            yield return null;
        }
        //_time= 0;
        Debug.Log("��");
        lr.positionCount = 0;
        lr.material.color = tmp;
    }





}
