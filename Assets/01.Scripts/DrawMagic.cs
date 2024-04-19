using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawMagic : MonoBehaviour
{

    [SerializeField] GameObject magic;
    [SerializeField] GameObject explosion;

    ParticleSystem particle => explosion.GetComponent<ParticleSystem>();
    
    public Transform _magicStart;
    public List<GameObject> magicList;  //  마법의 양
    [SerializeField] private List<LineRenderer> lrList; //  계속 라인렌더를 찾기 위해 GetComponent를 쓰면 그러니 미리 잡아 놓고 시작하기 위한 라인렌더러
  
    

    LineRenderer prevLr;    //  마법이 사라질 때 사라지는 애의 라인 랜더러
    LineRenderer currentLR; //  현재 그려지고 있는 라인 랜더러

    
    [SerializeField] float _dis;    //  마법이 그려질 떄 서로의 현재 거리
    public float _minDis; //  마법이 그려질 떄 서로의 거리 기준

    public Vector3 prev;   //  마법을 그릴 때 시작점을 표시하기 위해

    public Color baseColor;
    public Material mat;
    void Start()
    {
        explosion.SetActive(false);
        foreach (GameObject magic in magicList)
        {
            LineRenderer tmplr = magic.GetComponent<LineRenderer>();
            tmplr.material = mat;
            lrList.Add(tmplr);
        }
        
        magic = magicList[0];
        baseColor = magic.GetComponent<LineRenderer>().material.color;
        currentLR = lrList[0];
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {            
            // prev = transform.position;
            magicList[0].SetActive(true);     
            SetLRPosition();                  
        }
        if (Input.GetMouseButton(0))
        {
            UpdateLRPosition();
        }

        else if (Input.GetMouseButtonUp(0))
        {
            SwitchMagic();
            // StartCoroutine(EndMagic());
        }
    }

    void UpdateLRPosition()
    {
            _dis = Vector3.Distance(prev, transform.position);
            if (_dis > _minDis)
            {
                SetLRPosition();

                prev = transform.position;
            }

            if (_dis < _minDis && currentLR.positionCount > 0)
            {
                currentLR.SetPosition(currentLR.positionCount - 1, transform.position);
            }
    }

    void SetLRPosition()
    {
        currentLR.positionCount++;
        currentLR.SetPosition(currentLR.positionCount -1, transform.position);
        if(currentLR.positionCount==1)
        {
            SetLRPosition();
        }
        
    }
    void SwitchMagic()
    {
        magicList.Add(magic);
        magicList.RemoveAt(0);

        lrList.Add(currentLR);
        lrList.RemoveAt(0);

        magic = magicList[0];
        currentLR = lrList[0];
    }
    public float _magicEndTime = 5;

    IEnumerator EndMagic()
    {
        SwitchMagic();
        
        prevLr = lrList[lrList.Count-1];
        Color tmp = prevLr.material.color;
        float time = 0;
        float lerpMaxTime = _magicEndTime / Time.fixedDeltaTime;
        // float lerpMaxTime = _magicEndTime/Time.fixedDeltaTime;
        Color matColor = prevLr.material.color;
        while (time < _magicEndTime)
        {
            time += Time.fixedDeltaTime;
            float alpha = Mathf.Lerp(matColor.a, 0, time / lerpMaxTime);

            matColor.a = alpha;

            prevLr.material.color = matColor;
            yield return null;
        }
        Vector3[] dd = new Vector3[prevLr.positionCount];
        prevLr.GetPositions(dd);

        Vector3 failure = Vector3.zero;
        for (int i = 0; i < prevLr.positionCount; i++)
        {
            failure += dd[i];
        }
        // explosion.transform.position = failure/lr.positionCount;
        // explosion.SetActive(true);
        // particle.Play();
        prevLr.positionCount = 0;
        // prevLr.material.color = tmp;
        prevLr.material.color = baseColor;
        prevLr.gameObject.SetActive(false);



    }

}
