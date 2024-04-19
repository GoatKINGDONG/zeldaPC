using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : MonoBehaviour
{
    [SerializeField] protected float _magicEndTime;
    [SerializeField] protected float _magicDistance;
    [SerializeField] protected Material _magicMaterial;
    [SerializeField] protected List<GameObject> _magicList;
    [SerializeField] protected List<LineRenderer> _lrList;

    public float MagicEndTime{get{return _magicEndTime;}}
    public float MagicDistance{get{return _magicDistance;}}
    public Material MagicMaterial{get{return _magicMaterial;}}
    public List<GameObject> MagicList{get{return _magicList;}}
    public List<LineRenderer> LRList{get{return _lrList;}}
}
