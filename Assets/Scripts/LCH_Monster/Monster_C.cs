using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_C : Monster
{
    [SerializeField] private GameObject atkPrefab;
    [SerializeField] private GameObject explosionPrefab;

    [Header("공격 발동까지 걸리는 시간")]
    [SerializeField] private float growDuration;  // 커지는 데 걸리는 시간
    [Header("바닥 범위 설정")]
    [SerializeField] private Vector3 targetScale;
    [Header("폭발 크기 설정")]
    [SerializeField] private Vector3 explosionSize;

    public float LastAttackTime { get; set; }

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
        atkPrefab.transform.localScale = targetScale;
        explosionPrefab.transform.localScale = explosionSize;
    }

    public void StartAtk()
    {
        if (atkPrefab != null)
        {
            IsAttack = true;
            atkPrefab.SetActive(true);
            StartCoroutine(GrowOverTime(atkPrefab));
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

        //if (Mon_Common_Hp_Remain <= 0)
        //{
        //    atkPrefab.SetActive(false);
        //    explosionPrefab.SetActive(false);
        //}
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
