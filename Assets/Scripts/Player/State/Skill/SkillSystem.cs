using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] GameObject[] Skill_Effect;
    [SerializeField] Skill_Shield Skill_Shield;

    public void SetActive_Skiil_Effect(int index, bool isActive)
    {
        index = index - 1;
        Skill_Effect[index].SetActive(isActive);
    }

    public void Active_Shield(float value)
    {
        Skill_Shield.ApplyShield(value);
    }
}
