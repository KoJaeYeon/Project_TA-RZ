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


    public void SetStage(StageType newStage)
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
