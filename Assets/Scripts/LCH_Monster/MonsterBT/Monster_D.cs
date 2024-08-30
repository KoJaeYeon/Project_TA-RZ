using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_D : Monster
{
    public bool IsAttack { get; set; }
    public float LastAttackTime { get; set; }
    public bool isDashing { get; set; }

    // 돌진 중인지 여부
    public bool IsDrawDash { get; set; }

    [SerializeField] private float dashAttackDistance; // 돌진할 거리
    [SerializeField] private float dashSpeed;          // 돌진 속도
    private Vector3 targetPosition;                    // 저장된 플레이어 위치
    private Collider collider;
    Animator animator;
    LayerMask _playerLayer;

    private Monster_D_DashUI dashUi;
    protected override void Awake()
    {
        base.Awake();
        idStr = "E104";
        collider = GetComponent<Collider>();
        animator = GetComponentInChildren<Animator>();
        dashUi = GetComponentInChildren<Monster_D_DashUI>();
    }

    public override void ApplyKnockback(float knockbackDuration, Transform attackerTrans)
    {
        if (isDashing == true)
        {
            return;
        }
        base.ApplyKnockback(knockbackDuration, attackerTrans);
    }

    public override void Hit(float damage, float paralysisTime, Transform transform)
    {
        base.Hit(damage, 0, transform);
        if (Mon_Common_Hp_Remain <= 0)
        {
            dashUi.OnDashEnd();
        }
    }

    public void CheckBeforeDash()
    {
        StartCoroutine(ReadyToDash());
    }

    public void OnAtk(Transform playerTransform)
    {
        IsAttack = true;
        collider.isTrigger = true;
        targetPosition = playerTransform.position;
        StartCoroutine(DashToTarget());
    }

    private IEnumerator DashToTarget()
    {
        IsDrawDash = true;
        isDashing = true;
        animator.SetBool("Atk",true);
        dashUi.OnDash();
        Vector3 startPosition = transform.position;
        Vector3 dashDirection = (targetPosition - startPosition).normalized;


        float distanceCovered = 0f;
        while (distanceCovered < dashAttackDistance && IsAttack)
        {
            float step = dashSpeed * Time.deltaTime;
            transform.position += dashDirection * step;
            distanceCovered += step;

            RaycastHit hit;
            string[] mask = new string[] { "Player", "Ghost", "Environment" };
            _playerLayer = LayerMask.GetMask(mask);

            if (Physics.Raycast(transform.position, dashDirection, out hit, dashAttackDistance,_playerLayer))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Player.Hit(Mon_Common_Damage, 0, this.transform);
                    StopDash();
                    yield break;
                }
                else if (hit.collider.gameObject.layer.Equals(LayerMask.NameToLayer("Environment")))
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
        animator.SetTrigger("beforeAtk");
        IsDrawDash = true;
        float duration = 1f;  
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            dashUi.DashGauge.fillAmount = Mathf.Lerp(0, 1, elapsedTime / duration);

            yield return null;

        }
        IsDrawDash = false;
        dashUi.DashGauge.fillAmount = 1f;  
    }



    private void StopDash()
    {
        dashUi.OnDashEnd();
        LastAttackTime = Time.time;
        animator.SetBool("Atk", false);
        isDashing = false;
        IsAttack = false;
        collider.isTrigger = false;
    }

    
}
