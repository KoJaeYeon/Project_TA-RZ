using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_C : Monster
{
    [SerializeField] private GameObject atkPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject bombPrefab;

    [Header("공격 발동까지 걸리는 시간")]
    [SerializeField] private float growDuration;  // 커지는 데 걸리는 시간
    [Header("바닥 범위 설정")]
    [SerializeField] private Vector3 targetScale;
    [Header("폭발 크기 설정")]
    [SerializeField] private Vector3 explosionSize;

    public float LastAttackTime { get; set; }
    public float GrowDuration { get { return growDuration; } }

    public bool IsAttack { get; set; }
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        idStr = "E103";
        var monsterC = explosionPrefab.GetComponent<Monster_C_Atk>();
        if (monsterC != null)
        {
            monsterC.Initialize(this);
        }
        if (atkPrefab == null)
        {
            Debug.Log("어택프리팹이없슴");
        }
        atkPrefab.transform.localScale = targetScale;
        explosionPrefab.transform.localScale = explosionSize;
    }

    public void StartAtk()
    {
        if (atkPrefab != null)
        {
            Debug.Log("Monster_C StartAtk called at position: " + transform.position);
            IsAttack = true;
            atkPrefab.SetActive(true);
            StartCoroutine(GrowOverTime(atkPrefab));
        }
        else
        {
            Debug.LogError("Monster_C atkPrefab is null");
        }
    }


    public override void Hit(float damage, float paralysisTime, Transform transform)
    {
        

        if (IsAttack == true)
        {
            base.Hit(damage, 0, transform);
        }
        else
        {
            base.Hit(damage, paralysisTime, transform);
        }

        if (Mon_Common_Hp_Remain <= 0)
        {
            bombPrefab.transform.SetParent(transform);
            bombPrefab.SetActive(false);
            atkPrefab.SetActive(false);
            explosionPrefab.SetActive(false);
        }
    }

    public override void ApplyKnockback(float knockbackDuration, Transform attackerTrans)
    {
        if (IsAttack == true)
        {
            return;
        }

        base.ApplyKnockback(knockbackDuration, attackerTrans);

    }
    public void PlayExplosion()
    {
        if (explosionPrefab != null)
        {
            explosionPrefab.SetActive(true);
            LastAttackTime = Time.time;

            var particle = explosionPrefab.GetComponent<ParticleSystem>();

            if (particle != null)
            {
                particle.Play();
                StartCoroutine(WaitForParticleToFinish(particle, explosionPrefab));
            }
        }
    }

    private IEnumerator GrowOverTime(GameObject atkObject)
    {
        float timeElapsed = 0f;

        while (timeElapsed < growDuration)
        {
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        if (timeElapsed >= growDuration)
        {
            atkObject.SetActive(false);
            PlayExplosion();
        }
    }

    private IEnumerator WaitForParticleToFinish(ParticleSystem particle, GameObject explosion)
    {
        IsAttack = false;
        yield return new WaitForSeconds(growDuration);
        //yield return new WaitUntil(() => !particle.isPlaying);

        explosion.SetActive(false);
    }
}
