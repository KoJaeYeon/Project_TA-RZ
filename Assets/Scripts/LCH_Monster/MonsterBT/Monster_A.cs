using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Monster_A : Monster, IHit
{
    [SerializeField] private GameObject atkPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [Header("공격 발동까지 걸리는 시간")]
    [SerializeField] private float growDuration;  // 커지는 데 걸리는 시간
    private Vector3 targetScale = new Vector3(2f, 0.1f, 2f);  // 목표 크기

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
        Vector3 initialScale = atkObject.transform.localScale;
        float timeElapsed = 0f;

        while (timeElapsed < growDuration)
        {
            atkObject.transform.localScale = Vector3.Lerp(initialScale, targetScale, timeElapsed / growDuration);
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
