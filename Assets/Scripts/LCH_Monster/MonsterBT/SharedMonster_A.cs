using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedMonster_A : SharedVariable<Monster_A>
    {
        public static implicit operator SharedMonster_A(Monster_A value) { return new SharedMonster_A { mValue = value }; }
    }
}