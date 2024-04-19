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

    public float _time = 0;
    bool _isEnd = false;

    private void Awake()
    {

        _wand = transform.parent.gameObject;
        _magicWand = _wand.GetComponent<MagicWand>();
        _drawMagic = _wand.GetComponent<DrawMagic>();
        _minDis = _magicWand.MagicDistance;
        _magicEndTime = _magicWand.MagicEndTime;
        lr = GetComponent<LineRenderer>();
    }
    private void OnEnable()
    {
        _time = 0;
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
            // StartCoroutine(c_EndMagic());
            _isEnd = true;

        }
        if (_isEnd == true)
        {
            Test();
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



    void Test()
    {
        _time += Time.deltaTime; // 경과 시간을 업데이트

        // 경과 시간이 duration보다 작을 때만 실행
        if (_time < _magicEndTime)
        {
            // 시작 알파값은 1(완전 불투명), 목표 알파값은 0(완전 투명)
            float startAlpha = 1f;
            float targetAlpha = 0f;

            // 현재 시간에 따라 알파값을 보간
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, _time / _magicEndTime);

            // 보간된 알파값을 게임 오브젝트에 적용
            Color color = lr.material.color;
            color.a = alpha;
            lr.material.color = color;
        }
        else
        {
            lr.positionCount = 0;
            lr.material.color = _magicWand.MagicMaterial.color;
            gameObject.SetActive(false);
            _isEnd = false;
        }

    }


}
