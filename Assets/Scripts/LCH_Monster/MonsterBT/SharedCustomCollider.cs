using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedCustomCollider : SharedVariable<BoxCollider>
    {
        public static implicit operator SharedCustomCollider(BoxCollider value) { return new SharedCustomCollider { mValue = value }; }
    }
}