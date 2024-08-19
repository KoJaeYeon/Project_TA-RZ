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

    private readonly int _hashActive = Animator.StringToHash("isActive");
    private readonly int _hashAttackReady = Animator.StringToHash("AttackReady");
    private readonly int _hashAttack = Animator.StringToHash("Attack");
    private readonly int _hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();

        _boss = GetComponentInParent<BossController>();
        //_attackMark = GetComponentInChildren<SpriteRenderer>();
        //_attackMark.gameObject.SetActive(false);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            rootState = RootState.Emerge;
            RootAttack(transform.position);
            Debug.Log("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MarkSmash(transform.rotation);
            Debug.Log("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RootSmash();
            Debug.Log("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HideRoot();
            Debug.Log("4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Die();
            Debug.Log("5");
        }
    }

    public void RootAttack(Vector3 spawnPos)
    {
        if (rootState == RootState.Hide) return;

        transform.position = spawnPos;
        rootState = RootState.Emerge;
        _anim.SetBool(_hashActive, true);
    }

    public void MarkSmash(Quaternion rot)
    {
        if (rootState == RootState.Hide) return;

        transform.rotation = rot;
        _anim.SetTrigger(_hashAttackReady);
        //_attackMark.gameObject.SetActive(true);
    }
    public void RootSmash()
    {
        if (rootState == RootState.Hide) return;

        _anim.SetTrigger(_hashAttack);
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
                //HideRoot();
            }
        }
    }

    private void HideRoot()
    {
        if (rootState == RootState.Hide) return;

        _anim.SetBool(_hashActive, false);
        rootState = RootState.Hide;
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
