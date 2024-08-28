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

    private void Update()
    {
        // 임시로 설정함
        Mon_Common_Range = 10;
        Mon_Common_CoolTime = 4;
    }
    protected override void Awake()
    {
        base.Awake();
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

        atkObject.transform.localScale = targetScale; // Ensure final scale is set
        atkObject.SetActive(false);
        PlayExplosion(atkObject.transform);
    }

    public void PlayExplosion(Transform targetTrans)
    {
        if (explosionPrefab != null)
        {
            explosionPrefab.transform.parent = null;
            explosionPrefab.transform.position = targetTrans.position;
            explosionPrefab.SetActive(true);
            var particle = explosionPrefab.GetComponent<ParticleSystem>();

            if (particle != null)
            {
                particle.Play();
                StartCoroutine(WaitForParticleToFinish(particle, explosionPrefab));
            }
        }
    }

    private IEnumerator WaitForParticleToFinish(ParticleSystem particle, GameObject explosion)
    {
        yield return new WaitForSeconds(2f);
        //yield return new WaitUntil(() => !particle.isPlaying);

        explosion.SetActive(false);
    }
    //public override void Hit(float damage, float paralysisTime, Transform transform)
    //{
    //    base.Hit(damage, paralysisTime, transform);
    //    if (Mon_Common_Stat_Hp <= 0)
    //    {
    //        OnDestroy();
    //    }
    //}
    //private void OnDestroy()
    //{
    //    atkPrefab.SetActive(false);
    //    explosionPrefab.SetActive(false);
    //}
}
