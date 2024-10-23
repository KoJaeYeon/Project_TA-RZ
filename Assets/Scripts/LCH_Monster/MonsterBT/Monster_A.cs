using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Monster_A : Monster, IHit
{
    [SerializeField] private GameObject atkPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private MonsterA_Ex monA_Ex;
    [SerializeField] private Monster_A_atkEx atkEx;
    [Header("공격 발동까지 걸리는 시간")]
    [SerializeField] public float growDuration;  // 커지는 데 걸리는 시간
    public float GrowDuration { get { return growDuration; } }
    [Header("바닥 범위 설정")]
    [SerializeField] private Vector3 targetScale;
    public Vector3 TargetScale { get { return targetScale; } }
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
    }

    public void StartAtk()
    {
        if (atkPrefab != null)
        {
            Vector3 playerPosition = Player.transform.position;

            atkPrefab.transform.position = playerPosition;
            atkPrefab.SetActive(true);
            atkPrefab.transform.parent = null;
                        
            atkEx.StartCoroutine(GrowOverTime(atkPrefab));
        }
    }

    public override void Hit(float damage, float paralysisTime, Transform transform)
    {
        base.Hit(damage, paralysisTime, transform);
        //if (Mon_Common_Hp_Remain <= 0)
        //{
        //    atkPrefab.transform.parent = this.transform;
        //    explosionPrefab.transform.parent = this.transform;
        //    atkPrefab.SetActive(true);
        //    explosionPrefab.SetActive(false);
        //    targetPrefab.SetActive(false);
        //}
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

                particle.Play();
                monA_Ex.StartCoroutine(WaitForParticleToFinish(particle, explosionPrefab));

            
        }        
    }

    private IEnumerator GrowOverTime(GameObject atkObject)
    {
        float timeElapsed = 0f;

        while (timeElapsed < growDuration)
        {
            Debug.Log("asd");
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        Debug.Log("cdf");
        PlayExplosion(atkObject.transform);
        atkObject.SetActive(false);
    }

    private IEnumerator WaitForParticleToFinish(ParticleSystem particle, GameObject explosion)
    {
        yield return new WaitForSeconds(growDuration);
        //yield return new WaitUntil(() => !particle.isPlaying);

        explosion.SetActive(false);
    }
    
}
