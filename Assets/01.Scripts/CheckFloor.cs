using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CheckFloor : MonoBehaviour
{
    [SerializeField] float _maxDistance;
    [SerializeField] float _maxX;
    [SerializeField] float _maxY;
    [SerializeField] float _maxZ;
    [SerializeField] float _legDistance;
    Vector3 _box;
    private Color _rayColor = Color.red;
    RaycastHit hit;
  
 
    public void CheckGround(Transform Legs){
         if (Physics.BoxCast(Legs.position, _box/2, -Legs.up, out hit, Legs.rotation, _maxDistance))
        {            
            Debug.Log(hit.transform.gameObject.name);
            
            Gizmos.color = _rayColor;
            Gizmos.DrawRay(Legs.position, -Legs.up * hit.distance);
            Gizmos.DrawWireCube(hit.point, new Vector3(_maxX, _maxY, _maxZ));
            

            if (hit.distance < 0.1f)
            {
                Debug.Log("isGround가 빠르다");
                //PlayerControl.instance.isGround = true;                
            }
            else
            {
                //PlayerControl.instance.isGround = false;
            }
        }
    }
   
}
