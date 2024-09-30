using System.Collections;
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
    LayerMask _playerLayer;

    public Monster_D_DashUI dashUi { get; protected set; }
    protected override void Awake()
    {
        base.Awake();
        idStr = "E104";
        collider = GetComponent<Collider>();
    }

    public override void OnSetMonsterStat(float stat_Multiplier)
    {
        base.OnSetMonsterStat(stat_Multiplier);
        dashUi = GetComponentInChildren<Monster_D_DashUI>();
    }

    public override void ApplyKnockback(float knockbackDuration, Transform attackerTrans)
    {
        if (isDashing == true)
        {
            return;
        }
        else if(IsDrawDash == true)
        { 

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

    public void CheckBeforeDash(Transform playerTransform)
    {
        targetPosition = playerTransform.position;
    }

    public void OnAtk()
    {
        IsAttack = true;
        collider.isTrigger = true;        
        StartCoroutine(DashToTarget());
    }

    private IEnumerator DashToTarget()
    {
        IsDrawDash = true;
        isDashing = true;
        Anim.SetBool("Atk",true);
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

    private void StopDash()
    {
        dashUi.OnDashEnd();
        LastAttackTime = Time.time;
        Anim.SetBool("Atk", false);
        isDashing = false;
        IsAttack = false;
        collider.isTrigger = false;
    }

    
}
