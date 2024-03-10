using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 캐릭터의 발 위치값들의 중심점을 잡아서 
/// 바닥에 닿았는 지 체크하기 위한 스크립트
/// </summary> <summary>
/// 
/// </summary>
public class CheckFloor : MonoBehaviour
{
    //  본체
    // public Transform Player;
    // private PlayerControl _playerControl;
    // //  캐릭터의 발들 넣는 곳
    // [SerializeField] List<Transform> _foots;

    // //  발들의 중심
    // private Vector3 _centerFoot = Vector3.zero;
    // //  중간 값을 넣기 위한 임시 값
    // private Vector3 _tmp_legsTmp;

    // //  바닥에 닿았는 지 체크하는 박스의 크기
    // [SerializeField] private Vector3 _boxSize;

    // //  바닥인지 체크하기 위한 레이어
    // [SerializeField] private LayerMask _floorLayer;
    // //  바닥에 닿았는 지 체크하는 bool값
    // public bool isFloor;

    // //  바닥 혹은 벽들을 체크하는 콜라이더 ( 0개 이상이면 바닥에 닿았다는 뜻)
    // [SerializeField] Collider[] _floor_Colliders = PlayerControl.floorCollider;

    // private void Awake()
    // {
    //     _playerControl = Player.GetComponent<PlayerControl>();
    // }
    // private void Update()
    // {
    //     _playerControl.isFloor = CheckingFloor(); 
    // }

    // //  바닥 체크하는 함수
    // private bool CheckingFloor()
    // {
    //     _floor_Colliders = Physics.OverlapBox(CenterFoots(), _boxSize, Quaternion.identity, _floorLayer);
    //     if (_floor_Colliders.Length > 0)
    //     {
    //         isFloor = true;
    //     }
    //     else isFloor = false;

    //     return isFloor;
    // }

    // //  발들의 중심점( 박스의 위치)
    // private Vector3 CenterFoots()
    // {
    //     _tmp_legsTmp = Vector3.zero;
    //     for (int i = 0; i < _foots.Count; i++)
    //     {
    //         _tmp_legsTmp.x += _foots[i].transform.position.x;
    //         _tmp_legsTmp.y += _foots[i].transform.position.y;
    //         _tmp_legsTmp.z += _foots[i].transform.position.z;
    //     }
    //     transform.position = _tmp_legsTmp / 2;

    //     return transform.position;
    // }


}
