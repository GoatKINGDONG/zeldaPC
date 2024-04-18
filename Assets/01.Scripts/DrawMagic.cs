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
    public List<GameObject> magicList;
    [SerializeField] private List<LineRenderer> lrList;

    LineRenderer lr;
    LineRenderer prevLr;
    LineRenderer currentLR;

    [SerializeField] float _dis;
    [SerializeField] float _minDis;
    Vector3 prev;

    void Start()
    {
        // prev = transform.position;
        explosion.SetActive(false);
        foreach(GameObject magic in magicList){
            LineRenderer tmplr = magic.GetComponent<LineRenderer>();
            lrList.Add(tmplr);
        }
        
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            prev = transform.position;
            
            magicList[0].SetActive(true);
            // magic = magicList[0];
            // lr = magic.GetComponent<LineRenderer>();
            lr = lrList[0];
            // _dis = 0;
        }
        else if (Input.GetMouseButton(0))
        {
            _dis = Vector3.Distance(prev, transform.position);
            if (_dis > _minDis)
            {
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, transform.position);

                prev = transform.position;
            }

            if(_dis < _minDis && lr.positionCount > 0)
            {
                lr.SetPosition(lr.positionCount - 1, transform.position);
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            // magic = magicList[1];
            magicList.Add(magicList[0]);
            magicList.Remove(magicList[0]);
            lrList.Add(lrList[0]);
            lrList.Remove(lrList[0]);

            // magic = magicList[0];

            
            // StartCoroutine(EndMagic());
        }
    }

    public float _magicEndTime = 5;
     IEnumerator EndMagic()
    {
        prevLr = magicList[magicList.Count-1].GetComponent<LineRenderer>();
        // magicList.Remove(this.gameObject);
        // magicList.Add(this.gameObject);
        Color tmp = prevLr.material.color;
        float time = 0;
        float lerpMaxTime = _magicEndTime/Time.fixedDeltaTime;
        // float lerpMaxTime = _magicEndTime/Time.fixedDeltaTime;
        Color matColor = prevLr.material.color;
        while (time < _magicEndTime)
        {
            time += Time.fixedDeltaTime;
            float alpha = Mathf.Lerp(matColor.a,0, time/lerpMaxTime);

            matColor.a = alpha;

            prevLr.material.color = matColor;
            yield return null;
        }
        Vector3[] dd = new Vector3[prevLr.positionCount];
        prevLr.GetPositions(dd);
        
        Vector3 failure = Vector3.zero;
        for(int i =0; i < prevLr.positionCount; i++){
            failure += dd[i];
        }
        // explosion.transform.position = failure/lr.positionCount;
        // explosion.SetActive(true);
        // particle.Play();
        prevLr.positionCount = 0;
        prevLr.material.color = tmp;
        magicList[magicList.Count-1].SetActive(false);
        
        
        
    }

}
