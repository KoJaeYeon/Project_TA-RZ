using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] GameObject[] Skill_Effect;

    public void SetActive_Skiil_Effect(int index, bool isActive)
    {
        Debug.Log(index);
        Debug.Log(Skill_Effect.Length);
        Skill_Effect[index].SetActive(isActive);
    }

}
