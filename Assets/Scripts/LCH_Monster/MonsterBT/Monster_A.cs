using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Monster_A : Monster, IHit
{
    [SerializeField] private GameObject atkPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject targetPrefab;
    [Header("공격 발동까지 걸리는 시간")]
    [SerializeField] private float growDuration;  // 커지는 데 걸리는 시간
    [Header("바닥 범위 설정")]
    [SerializeField] private Vector3 targetScale;
    [Header("폭발 크기 설정")]
    [SerializeField] private Vector3 explosionSize;
    public float LastAttackTime { get; set; }
    
    protected override void Awake()
    {
        base.Awake();
        idStr = "E102";
        var monsterAEx = explosionPrefab.GetComponent<MonsterA_Ex>();
        if (monsterAEx != null)
        {
            monsterAEx.Initialize(this);
        }
        atkPrefab.transform.localScale = targetScale;
        explosionPrefab.transform.localScale = explosionSize;
    }

    public void StartAtk()
    {
        if (atkPrefab != null)
        {
            Vector3 playerPosition = Player.transform.position;

            atkPrefab.transform.position = playerPosition;
            atkPrefab.SetActive(true);
            atkPrefab.transform.parent = null;

            StartCoroutine(GrowOverTime(atkPrefab));
        }
    }

    public override void Hit(float damage, float paralysisTime, Transform transform)
    {
        base.Hit(damage, paralysisTime, transform);
        if (Mon_Common_Hp_Remain <= 0)
        {
            atkPrefab.transform.parent = this.transform;
            explosionPrefab.transform.parent = this.transform;
            atkPrefab.SetActive(true);
            explosionPrefab.SetActive(false);
            targetPrefab.SetActive(false);
        }
    }
    

    public void PlayExplosion(Transform targetTrans)
    {
        if (explosionPrefab != null)
        {
            explosionPrefab.transform.parent = null;
            explosionPrefab.transform.position = targetTrans.position;
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
            PlayExplosion(atkObject.transform);

        }
    }

    private IEnumerator WaitForParticleToFinish(ParticleSystem particle, GameObject explosion)
    {
        yield return new WaitForSeconds(growDuration);
        //yield return new WaitUntil(() => !particle.isPlaying);

        explosion.SetActive(false);
    }
    
}
