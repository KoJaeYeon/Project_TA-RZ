using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Stage : MonoBehaviour
{
    [Inject]
    private MapManager _mapManager;
    [Inject]
    private DataManager _dataManager;

    [SerializeField] 
    private Transform[] _mapTrans;

    private void Awake()
    {
        
    }

    public void GenerateMonster()
    {

    }
}
