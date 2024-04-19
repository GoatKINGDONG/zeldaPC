using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{

    [SerializeField] private GameObject _magic_Start;
    [SerializeField] private DrawMagic _drawMagic;
    [SerializeField] float _minDis;
    
    [SerializeField] Material _mat;
    LineRenderer lr;

    Vector3 prev;
    
    private void Start()
    {
        _magic_Start = transform.parent.gameObject;
        _drawMagic = _magic_Start.GetComponent<DrawMagic>();
        _minDis = _drawMagic._minDis;
        lr = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable() 
    {
        // lr.material = _drawMagic.mat;
        prev = _drawMagic.prev;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            SetLRPosition();
        }
        if (Input.GetMouseButton(0))
        {
            if (lr.positionCount < 1)
            {
                Debug.Log("첫 클릭");
                prev = _magic_Start.transform.position;

                for (int i = 0; i < 2; i++)
                { SetLRPosition(); }
            }
            else if(lr.positionCount>1){
                UpdateLRPosition();
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {

        }

    }

    void UpdateLRPosition()
    {
        float _dis = Vector3.Distance(prev, _magic_Start.transform.position);
        if (_dis > _minDis)
        {
            SetLRPosition();
            prev = _magic_Start.transform.position;
        }

        if (_dis < _minDis && lr.positionCount > 0)
        {
            lr.SetPosition(lr.positionCount - 1, _magic_Start.transform.position);
        }
    }


    void SetLRPosition()
    {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount -1, transform.position);
        if(lr.positionCount==1)
        {
            SetLRPosition();
        }
    }

    
    public float _magicEndTime = 5;

    IEnumerator EndMagic()
    {
        // SwitchMagic();
        
        // prevLr = lrList[lrList.Count-1];
        Color tmp = lr.material.color;
        float time = 0;
        float lerpMaxTime = _magicEndTime / Time.fixedDeltaTime;
        // float lerpMaxTime = _magicEndTime/Time.fixedDeltaTime;
        Color matColor = lr.material.color;
        while (time < _magicEndTime)
        {
            time += Time.fixedDeltaTime;
            float alpha = Mathf.Lerp(matColor.a, 0, time / lerpMaxTime);

            matColor.a = alpha;

            lr.material.color = matColor;
            yield return null;
        }
        Vector3[] dd = new Vector3[lr.positionCount];
        lr.GetPositions(dd);

        Vector3 failure = Vector3.zero;
        for (int i = 0; i < lr.positionCount; i++)
        {
            failure += dd[i];
        }
        // explosion.transform.position = failure/lr.positionCount;
        // explosion.SetActive(true);
        // particle.Play();
        lr.positionCount = 0;
        // prevLr.material.color = tmp;
        lr.material.color = _drawMagic.baseColor;
        this.gameObject.SetActive(false);



    }
}
