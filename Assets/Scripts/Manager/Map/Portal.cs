using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class Portal : MonoBehaviour
{
    #region InJect
    [Inject] private UIEvent _uiEvent;
    [Inject] private MapManager _mapManager;
    #endregion;

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            if(player != null)
            {
                _uiEvent.OnGameUI();
            }
            
            this.gameObject.SetActive(false);
        }
    }

    #region JYCode
    //[SerializeField] StageType StageType;
    //[SerializeField] Sprite[] StageSpriteArr;


    //[SerializeField] SpriteRenderer StageSpriteObject;
    //[Inject] Player player;
    //private void Start()
    //{        
    //    ChangeStageSprite();
    //}

    //private void Update()
    //{
    //    if(player != null)
    //    {
    //        SpritePlayerLook();
    //    }
    //    else
    //    {
    //        Debug.LogError("Jenject 오류! 플레이어가 없음! Context 확인 필요!");
    //    }

    //}

    ///// <summary>
    ///// 스테이지 타입을 변경해주는 함수
    ///// </summary>
    ///// <param name="stageType"></param>
    //public void OnChangeStageType(StageType stageType)
    //{
    //    StageType = stageType;
    //    ChangeStageSprite();
    //}


    ///// <summary>
    ///// 포탈의 이미지가 플레이어를 바라보는 함수
    ///// </summary>
    //void SpritePlayerLook()
    //{
    //    StageSpriteObject.transform.LookAt(Camera.main.transform);
    //    Vector3 LookPlayerAng = StageSpriteObject.transform.eulerAngles;
    //    LookPlayerAng.x = 0;
    //    LookPlayerAng.z = 0;
    //    StageSpriteObject.transform.eulerAngles = LookPlayerAng;
    //}

    ///// <summary>
    ///// 플레이어가 포탈에 입장할 때 스테이지 변경
    ///// </summary>
    //void ChangeStage()
    //{
    //    Debug.Log("Change Stage!");
    //}

    ///// <summary>
    ///// 다음 스테이지를 알려주는 이미지로 변경
    ///// </summary>
    //void ChangeStageSprite()
    //{
    //    int stageTypeIndex = (int)StageType;
    //    StageSpriteObject.sprite = StageSpriteArr[stageTypeIndex];
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Player"))
    //    {
    //        Debug.Log("Player Enter");
    //        ChangeStage();
    //    }
    //}
    #endregion
}
