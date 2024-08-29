using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_D : Monster
{
    public bool IsAttack { get; set; }
    public float LastAttackTime { get; set; }

    [SerializeField] private float dashAttackDistance; // 돌진할 거리
    [SerializeField] private float dashSpeed;          // 돌진 속도
    private Vector3 targetPosition;                    // 저장된 플레이어 위치
    private Collider collider;
    private bool isDashing;                            // 돌진 중인지 여부
    Animator animator;
    protected override void Awake()
    {
        base.Awake();
        idStr = "E104";
        collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
    }

    public override void ApplyKnockback(float knockbackDuration, Transform attackerTrans)
    {
        base.ApplyKnockback(knockbackDuration, attackerTrans);
    }

    public override void Hit(float damage, float paralysisTime, Transform transform)
    {
        base.Hit(damage, paralysisTime, transform);
        StopDash(); 
    }

    public void OnAtk(Transform playerTransform)
    {
        IsAttack = true;
        collider.isTrigger = true;
        isDashing = false;

        targetPosition = playerTransform.position;
        StartCoroutine(ReadyToDash());
        StartCoroutine(DashToTarget());
    }

    private IEnumerator DashToTarget()
    {
        isDashing = true;
        animator.SetBool("Atk",true);
        Vector3 startPosition = transform.position;
        Vector3 dashDirection = (targetPosition - startPosition).normalized;

        float distanceCovered = 0f;

        while (distanceCovered < dashAttackDistance && IsAttack)
        {
            float step = dashSpeed * Time.deltaTime;
            transform.position += dashDirection * step;
            distanceCovered += step;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, dashDirection, out hit, step))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // 플레이어와 충돌 시
                    Player.Hit(Mon_Common_Damage,0,this.transform);
                    StopDash();
                    yield break;
                }
                else if (hit.collider.CompareTag("Obstacle"))
                {
                    StopDash();
                    yield break;
                }
            }

            yield return null;
        }

        StopDash();
    }
    public IEnumerator ReadyToDash()
    {
        //대쉬 UI 작성
        yield return new WaitForSeconds(5f);
    }
    private void StopDash()
    {
        animator.SetBool("Atk", false);
        isDashing = false;
        IsAttack = false;
        collider.isTrigger = false;
    }

    
}
