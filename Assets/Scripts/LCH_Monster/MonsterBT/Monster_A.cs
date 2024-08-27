using System.Collections;
using UnityEngine;
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

    public void StartAtk()
    {
        if (atkPrefab != null)
        {
            atkPrefab.transform.position = Player.transform.position;
            atkPrefab.SetActive(true);
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
        // 파티클이 재생되는 동안 대기
        yield return new WaitUntil(() => !particle.isPlaying);

        // 파티클 재생이 끝나면 오브젝트를 비활성화
        explosion.SetActive(false);
    }
}
