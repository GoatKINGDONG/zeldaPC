using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_Test : MonoBehaviour
{
    public Transform maicWand;
    // Start is called before the first frame update

    [SerializeField] bool _isMagicDrawing;
    [SerializeField] int _magicIndex = 0;
    [SerializeField] int _magicAmount = 1000;
    [SerializeField] float speed = 1.0f;
    LineRenderer lr;
    [SerializeField] List<Vector3> _magicTransform = new List<Vector3>();
    [SerializeField] List<Vector3> _magic = new List<Vector3>();
    [SerializeField] float _magicIndexDistance = 10f;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Start()
    {
        lr.positionCount = _magicAmount;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Slerp(transform.position, maicWand.transform.position, speed);

        ClickCheck();
        Test();
        
    }

    void ClickCheck()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            _magicTransform.Add(transform.position);
            _magicIndex++;
            _isMagicDrawing = true;                                    
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMagicDrawing = false;
            OnDrawMagic();
        }
    }

    void Test()
    {
        if(_isMagicDrawing == true)
        {
            _magicTransform.Add(transform.position);
            //  어떤 조건이 필요로 할까요?
            //  우선 특정값 이상이라면 ~ 현재의 위치값을 저장해주세요
            //  특정값?? 아하! 전 vector3 값과 현재 내 위치값의 길이가 ~이상이면 저장을 해주세요!            
            _magicIndex++;  //  계속 값이 올라가겠네?
            //  그러면 어쩔 때 올라가는 지 확인을 해야겠네요!
            Debug.Log(_magicIndex);
            lr.SetPosition(_magicIndex - 1, _magicTransform[_magicIndex-1]);
            //lr.SetPosition(_magicIndex, transform.position);  //  이 부분은 계속 이루어져야 합니다. 
                                                              //                                        //  재귀함수가 되어야할까? 몰루?

        }
        else { return; }
    }

    void OnDrawMagic()
    {
        _magicTransform.Add(transform.position);
        _magicIndex++;
        
        //if (_magicIndex > 1)    
        //{
        //    Vector3 tmp = _magicTransform[_magicIndex] - _magicTransform[_magicIndex-1];    
        //    if (Vector3.Magnitude(tmp) >= _magicIndexDistance)
        //    {
        //        lr.SetPosition(_magicIndex, transform.position);
                
        //    } 
        //}
        if(this._isMagicDrawing == false)
        {
            Vector3[] tmpMagic = new Vector3[_magicTransform.Count];
            _magic = new List<Vector3>(_magicTransform);
            _magicTransform.Clear();
            _magicIndex = 0;
        }
      
    }
}
