using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float _attackRange;

    private bool isDie = false;

    private Rigidbody _rb;
    private Animator _anim;

    [HideInInspector] public RootState rootState;
    private BossController _boss;
    private SpriteRenderer _attackMark;

    private readonly int _hashSmash = Animator.StringToHash("Smash");
    private readonly int _hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _boss = GetComponentInParent<BossController>();
        _attackMark = GetComponentInChildren<SpriteRenderer>();
        _attackMark.gameObject.SetActive(false);

        _maxHp = _boss._maxHp / 4f;
    }

    private void OnEnable()
    {
        rootState = RootState.Hide;

        _hp = _maxHp;    
    }

    private void Start()
    {
        StartCoroutine(CheckDistance());
    }

    public void RootAttack(Vector3 spawnPos)
    {
        if (rootState == RootState.Hide) return;

        transform.position = spawnPos;
        rootState = RootState.Emerge;
    }

    public void MarkSmash(Quaternion rot)
    {
        if (rootState == RootState.Hide) return;

        transform.rotation = rot;
        _attackMark.gameObject.SetActive(true);
    }
    public void RootSmash()
    {
        if (rootState == RootState.Hide) return;

        _anim.SetTrigger(_hashSmash);
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

        rootState = RootState.Hide;
    }

    public void Hurt(float damage)
    {
        _hp -= damage;
        _boss.Hurt(damage);
    }

    private void Die()
    { 
        isDie = true;
    }
}
