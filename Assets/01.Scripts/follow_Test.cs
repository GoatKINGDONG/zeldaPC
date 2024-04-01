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
            //  � ������ �ʿ�� �ұ��?
            //  �켱 Ư���� �̻��̶�� ~ ������ ��ġ���� �������ּ���
            //  Ư����?? ����! �� vector3 ���� ���� �� ��ġ���� ���̰� ~�̻��̸� ������ ���ּ���!            
            _magicIndex++;  //  ��� ���� �ö󰡰ڳ�?
            //  �׷��� ��¿ �� �ö󰡴� �� Ȯ���� �ؾ߰ڳ׿�!
            Debug.Log(_magicIndex);
            lr.SetPosition(_magicIndex - 1, _magicTransform[_magicIndex-1]);
            //lr.SetPosition(_magicIndex, transform.position);  //  �� �κ��� ��� �̷������ �մϴ�. 
                                                              //                                        //  ����Լ��� �Ǿ���ұ�? ����?

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
