using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class Boss_RushParticle : MonoBehaviour
{
    private ParticleSystem _ps;
    private List<ParticleSystem.Particle> _outside = new List<ParticleSystem.Particle>();

    private void Awake()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        // Outside 영역을 벗어난 파티클들을 가져옵니다.
        _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, _outside);

        // 파티클의 정보를 수정합니다.
        var mainModule = _ps.main;
        var colors = new List<Color32>(_outside.Count);
        var particles = new List<ParticleSystem.Particle>(_outside);

        for (int i = 0; i < particles.Count; i++)
        {
            var particle = particles[i];

            // 파티클의 알파값을 0으로 설정합니다.
            Color32 color = particle.GetCurrentColor(_ps);
            color.a = 0;
            particle.startColor = color;

            // 수정된 파티클 정보를 리스트에 업데이트합니다.
            particles[i] = particle;
        }

        // 수정된 파티클 리스트를 시스템에 적용합니다.
        _ps.SetTriggerParticles(ParticleSystemTriggerEventType.Outside, particles);
    }
}
