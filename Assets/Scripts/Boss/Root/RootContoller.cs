using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum RootState
{ 
    Hide,
    Emerge
}

public class RootContoller : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _maxHp;
    public float damage;
    [SerializeField] private float _attackTime;
    public float _attackRange;

    private bool isDie = false;

    private Rigidbody _rb;
    private Animator _anim;
    private Vector3 _defaultPos;

    [HideInInspector] public RootState rootState;
    private BossController _boss;
    [SerializeField] private GameObject _attackMark;

    private readonly int _hashActive = Animator.StringToHash("isActive");
    private readonly int _hashAttackReady = Animator.StringToHash("AttackReady");
    private readonly int _hashAttack = Animator.StringToHash("Attack");
    private readonly int _hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _defaultPos = transform.position;

        _boss = GetComponentInParent<BossController>();
        _attackMark.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        rootState = RootState.Hide;   
    }

    private void Start()
    {
        _maxHp = _boss._maxHp / 4f;
        _hp = _maxHp;
        StartCoroutine(CheckDistance());
    }

    public void RootAttack(Vector3 spawnPos)
    {
        if (rootState == RootState.Hide) return;

        transform.position = spawnPos;
        rootState = RootState.Emerge;
        _anim.SetBool(_hashActive, true);
    }

    public void MarkSmash(Vector3 targetPos)
    {
        if (rootState == RootState.Hide) return;

        transform.rotation = RotateToPlayer(targetPos);
        _anim.SetTrigger(_hashAttackReady);
        _attackMark.gameObject.SetActive(true);
    }
    public void RootSmash()
    {
        if (rootState == RootState.Hide) return;

        _anim.SetTrigger(_hashAttack);
        _attackMark.gameObject.SetActive(false);
    }

    private IEnumerator CheckDistance()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.3f);

            if (_hp <= 0)
            {
                Die();
                break; 
            }

            if (rootState == RootState.Hide) continue;

            float distance = Vector3.Distance(transform.position, _boss.PlayerPos());

            if (_attackRange < distance)
            {
                HideRoot();
            }
        }
    }

    private void HideRoot()
    {
        if (rootState == RootState.Hide) return;

        _anim.SetBool(_hashActive, false);
        rootState = RootState.Hide;
    }

    private Quaternion RotateToPlayer(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position);
        direction.y = 0;
        direction.Normalize();
        Quaternion rotation = Quaternion.LookRotation(direction);
        return rotation;
    }

    public void Hurt(float damage)
    {
        if (_hp - damage <= 0)
        {
            _boss.Hurt(_hp);
            _hp = 0;
            return;
        }

        _hp -= damage;
        _boss.Hurt(damage);
    }

    private void Die()
    { 
        isDie = true;
        _anim.SetTrigger(_hashDie);
    }
}
