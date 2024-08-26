using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] GameObject[] Skill_Effect;
    //[SerializeField] Skill_Shield Skill_Shield;

    public void SetActive_Skiil_Effect(int index, bool isActive)
    {
        index = index - 1;
        Skill_Effect[index].SetActive(isActive);
    }

    public void Active_Shield(float value)
    {
        //Skill_Shield.ApplyShield(value);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Monster"));
        foreach(var item in colliders)
        {
            Vector3 attackPos = item.transform.position;
            Vector3 directionToAttack = attackPos - transform.position;

            // 방향이 전방인지 확인 (플레이어의 전방은 transform.forward와 비교)
            float dotProduct = Vector3.Dot(transform.forward, directionToAttack.normalized);

            // dotProduct가 0보다 크면 전방
            if (dotProduct >= 0)
            {
                var ihit = item.GetComponent<IHit>();
                ihit.ApplyKnockback(value, transform);
            }

        }
    }
}
