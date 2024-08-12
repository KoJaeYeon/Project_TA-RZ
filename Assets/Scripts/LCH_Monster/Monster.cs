using System;
using UnityEngine;
using BehaviorDesigner.Runtime;

using Zenject;

public class Monster : MonoBehaviour
{
    [SerializeField] BehaviorTree Bt;
    
    [SerializeField] int MonsterHp;
    [Inject] public Player Player { get;}

    void Start()
    {
        
    }
    private void Update()
    {

       
    }

}
