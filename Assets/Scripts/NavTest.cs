using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    [SerializeField] Transform target;
    // Start is called before the first frame update
    void Start()
    {
        var nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
