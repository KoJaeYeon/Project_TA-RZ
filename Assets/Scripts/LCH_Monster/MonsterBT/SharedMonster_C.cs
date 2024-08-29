using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedMonster_C : SharedVariable<Monster_C>
    {
        public static implicit operator SharedMonster_C(Monster_C value) { return new SharedMonster_C { mValue = value }; }
    }
}