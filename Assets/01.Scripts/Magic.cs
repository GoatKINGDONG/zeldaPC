using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Magic : MonoBehaviour
{
    protected MagicWand _magicWand;

    protected private GameObject _wand;   //  지팡이
    protected private DrawMagic _drawMagic;  //  마법봉 제어하는 스크립트
    protected float _minDis; //  마법이 그려지기 위한 최소 길이
    protected float _magicEndTime;  //  마법이 사라질 떄 소요될 시간

    [SerializeField] Material _mat; //  마법을 그릴 때 이펙트

    LineRenderer lr;

    public Vector3 prev = Vector3.zero;

    private void Awake()
    {
        
        _wand = transform.parent.gameObject;
        _magicWand = _wand.GetComponent<MagicWand>();
        _drawMagic = _wand.GetComponent<DrawMagic>();
        _minDis = _magicWand.MagicDistance;
        _magicEndTime = _magicWand.MagicEndTime;
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (_drawMagic.Magic == this.gameObject)    //  혹여 빠르게 클릭이 되어 중복됨을 방지
        {
            if (Input.GetMouseButton(0))
            {
                if (lr.positionCount < 1)
                {
                    
                    SetLRPosition();
                    prev = _wand.transform.position;

                    for (int i = 0; i < 2; i++)
                    { SetLRPosition(); }
                }
                else if (lr.positionCount > 1)
                {
                    UpdateLRPosition();
                }
            }
            
        }

        if (Input.GetMouseButtonUp(0))
            {                
                StartCoroutine(c_EndMagic());
            }

    }

    void UpdateLRPosition() //  위치들의 거리를 비교하여 충족이 되면 마법이 그려짐(충족되지 않는다면 현재 위치가 마지막 지점으로 고정)
    {
        float _dis = Vector3.Distance(prev, _wand.transform.position);
        if (_dis > _minDis)
        {
            SetLRPosition();
            prev = _wand.transform.position;
        }

        if (_dis < _minDis && lr.positionCount > 0)
        {
            lr.SetPosition(lr.positionCount - 1, _wand.transform.position);
        }
    }


    void SetLRPosition()    //  마법을 직접적으로 그리는 부분
    {
        lr.positionCount++;
        lr.SetPosition(lr.positionCount - 1, transform.position);
        if (lr.positionCount == 1)
        {
            SetLRPosition();
        }
    }



    IEnumerator c_EndMagic()  //  마법이 사라지는 코루틴
    {
        float time = 0;
        float lerpMaxTime = _magicEndTime / Time.fixedDeltaTime;
        Color matColor = lr.material.color;
        while (time < _magicEndTime)
        {
            time += Time.fixedDeltaTime;
            float alpha = Mathf.Lerp(matColor.a, 0, time / lerpMaxTime);

            matColor.a = alpha;

            lr.material.color = matColor;
            if (alpha < 0.1f)
            {
                lr.material.color = matColor;
                gameObject.SetActive(false);
                lr.positionCount = 0;
                lr.material.color = _magicWand.MagicMaterial.color;
            }
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
        // lr.positionCount = 0;
        // prevLr.material.color = tmp;
 

    }
}
