using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedNavmesh : SharedVariable<NavMeshAgent>
    {
        public static implicit operator SharedNavmesh(NavMeshAgent value) { return new SharedNavmesh { mValue = value }; }
    }
}