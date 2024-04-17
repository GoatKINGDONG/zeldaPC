using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{

    LineRenderer lr;
    [SerializeField] private float _dis;
    [SerializeField] private float _minDis;
    [SerializeField] private float _magicEndTime;
    [SerializeField] Transform _wand_Magic_Start;

    [SerializeField] private List<GameObject> magicList;

    public bool isMagicEnd = false;
    private Vector3 prev;

    [SerializeField] private Material _mat;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        // lr = new LineRenderer();
        lr.material = _mat;
            prev = _wand_Magic_Start.position;

        magicList = _wand_Magic_Start.GetComponent<DrawMagic>().magicList;
    }

    void Update()
    {
        DrawMagic();
    }

    void DrawMagic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(this.gameObject != magicList[0])
            {
                this.gameObject.AddComponent<LineRenderer>();
            }
            else {
                
            }
        }
        else if (Input.GetMouseButton(0))
        {
            _dis = Vector3.Distance(prev, _wand_Magic_Start.position);
            if (_dis > _minDis && isMagicEnd == false)
            {
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, _wand_Magic_Start.position);
                prev = _wand_Magic_Start.position;
            }
            else
            {
                if (lr.positionCount > 0)
                {
                    lr.SetPosition(lr.positionCount - 1, _wand_Magic_Start.position);
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isMagicEnd= true;
            StartCoroutine(EndMagic());
        }
    }

    IEnumerator EndMagic()
    {
        lr.positionCount = lr.positionCount;
        magicList.Remove(this.gameObject);
        magicList.Add(this.gameObject);
        Color tmp = lr.material.color;
        float time = 0;
        float lerpMaxTime = _magicEndTime/Time.fixedDeltaTime;
        // float lerpMaxTime = _magicEndTime/Time.fixedDeltaTime;
        Color matColor = lr.material.color;
        while (time < _magicEndTime)
        {
            time += Time.fixedDeltaTime;
            float alpha = Mathf.Lerp(matColor.a,0, time/lerpMaxTime);
            // float alpha = Mathf.Lerp(matColor.a,0, _magicEndTime/time);
            matColor.a = alpha;

            lr.material.color = matColor;
            yield return null;
        }
        lr.positionCount = 0;
        lr.material.color = tmp;
        this.gameObject.SetActive(false);
        isMagicEnd = false;
        
        
    }
}
