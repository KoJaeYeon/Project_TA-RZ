using System.Collections;
using UnityEngine;
using Zenject;

public class Monster_A : Monster, IHit
{
    [SerializeField] private GameObject atkPrefab;
    [SerializeField] private GameObject explosionPrefab;
    [Inject] private PoolManager _poolManager;

    private float growDuration = 2.0f;  // 커지는 데 걸리는 시간
    private Vector3 targetScale = new Vector3(2f, 0.1f, 2f);  // 목표 크기

    private void Awake()
    {
        // Attack과 Explosion에 대한 Pool 생성
        _poolManager.CreatePool(atkPrefab, 1);
        _poolManager.CreatePool(explosionPrefab, 1);

        // 최초 시작 시 코루틴 실행
    }
    private void Update()
    {
        //임시로 설정함
        //쿨타임 값을 안가져오는데 이거 어케하지
        Mon_Common_Range = 10;
        Mon_Common_CoolTime = 4;
    }

    public void StartAtk()
    {
        Vector3 playerPosition = Player.transform.position;
        GameObject atkObject = _poolManager.DequeueObject(atkPrefab);
        atkObject.transform.position = playerPosition;
        
        StartCoroutine(GrowOverTime(atkObject));
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

        // 크기 조정이 완료된 후 오브젝트 비활성화
        if (atkObject.transform.localScale == targetScale)
        {
            atkObject.SetActive(false);
            PlayExplosion(atkObject.transform);
            yield return null;
        }
    }

    public void PlayExplosion(Transform targetTrans)
    {
        GameObject explosion = _poolManager.DequeueObject(explosionPrefab);
        explosion.transform.position = targetTrans.position;
        ParticleSystem particle = explosion.GetComponent<ParticleSystem>();

        if (particle != null)
        {
            particle.Play();

            // ParticleSystem이 멈춘 후 풀로 반환하도록 콜백 등록
            StartCoroutine(WaitForParticleToFinish(particle, explosion));
        }
        
    }

    private IEnumerator WaitForParticleToFinish(ParticleSystem particle, GameObject explosion)
    {
        // 파티클이 재생되는 동안 대기
        yield return new WaitUntil(() => !particle.isPlaying);

        // 파티클 재생이 끝나면 오브젝트를 풀로 반환
        _poolManager.EnqueueObject(explosion);
    }

}
