using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MagicWand))]
public class DrawMagic : MonoBehaviour
{
    MagicWand _magicWand;

    protected GameObject _magic;    //  이 스크립트에서 다룰 게임 오브젝트
    public GameObject Magic { get { return _magic; } }

    protected List<GameObject> magicList;  //  이 스크립트에서 다룰 게임 오브젝트를 미리 받아놓은 곳
    protected List<LineRenderer> lrList = new List<LineRenderer>(); //  계속 라인렌더를 찾기 위해 GetComponent를 쓰면 그러니 미리 잡아 놓고 시작하기 위한 라인렌더러

    protected LineRenderer currentLR; //  현재 그려지고 있는 라인 랜더러
    

    protected Vector3 _prev = Vector3.zero;   //  마법을 그릴 때 시작점을 표시하기 위해
    public Vector3 Prev { get { return _prev; } }



    private void Awake()
    {
        _magicWand = GetComponent<MagicWand>();
    }

    void Start()
    {
        magicList = _magicWand.MagicList;
        SetLR();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            magicList[0].SetActive(true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SwitchMagic();
        }
    }

    void SetLR()    //  라인렌더러를 GetComponent하기엔 부하가 걸리니 미리 받아놓기
    {
        foreach (GameObject _magic in magicList)
        {
            LineRenderer tmplr = _magic.GetComponent<LineRenderer>();
            tmplr.material = _magicWand.MagicMaterial;
            lrList.Add(tmplr);
        }

        _magic = magicList[0];
        currentLR = lrList[0];
    }


    void SwitchMagic()  //  마법을 다룰 오브젝트 풀링을 위하여 작성
    {
        magicList.Add(_magic);
        magicList.RemoveAt(0);

        lrList.Add(currentLR);
        lrList.RemoveAt(0);

        _magic = magicList[0];
        currentLR = lrList[0];
    }


}
