using UnityEngine;

namespace BehaviorDesigner.Runtime
{
    [System.Serializable]
    public class SharedMonster_D : SharedVariable<Monster_D>
    {
        public static implicit operator SharedMonster_D(Monster_D value) { return new SharedMonster_D { mValue = value }; }
    }
}