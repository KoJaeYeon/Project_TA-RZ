using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MapManager : MonoBehaviour
{
   

    [Header("Stage1")]
    [SerializeField] private GameObject _stage1;
    

    [Header("Stage2")]
    [SerializeField] private GameObject _stage2;
    

    [Header("Stage3")]
    [SerializeField] private GameObject _stage3;

    [Header("StageStartPosition")]
    [SerializeField] private Transform[] _startPosition;
    

    private void Start()
    {
        InitializeMapManager();
    }

    private void InitializeMapManager()
    {

    }

    public void SetStage(StageType newStage, GameObject playerObject)
    {
        Debug.Log("스테이지 시작");




    }

    private void ActiveObject()
    {

    }

    private void DeActiveObject()
    {

    }
}
