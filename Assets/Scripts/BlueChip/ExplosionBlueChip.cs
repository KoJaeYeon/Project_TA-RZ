using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBlueChip : BlueChip
{
    private event Action<float> _explosionAction;
    private Vector3 _exploPosition;
    private Vector3 _forward;

    public override void InitializeBlueChip(BlueChipSystem blueChipSystem, PC_BlueChip data)
    {
        _blueChipSystem = blueChipSystem;
        _data = data;
        _currentPower = _data.Att_damage;
        _targetLayer = LayerMask.GetMask("Monster");

        _exploPosition = new Vector3(0f, 1f, 1f);
    }

    public override void SetEffectObject(GameObject effectObject)
    {
        _effectObject = effectObject;

        _poolManager.CreatePool(_effectObject);
    }

    public override void ResetSystem()
    {
        _currentLevel = 0;
        _currentPower = 0f;

        _poolManager.AllDestroyObject(_effectObject);
    }

    public override void LevelUpBlueChip()
    {
        _currentLevel++;

        if(_currentLevel <= 2)
        {
            _currentPower += _data.Att_Damage_Lvup;
        }
    }

    public override void UseBlueChip(Transform transform, AttackType currentAttackType, bool isStart = true)
    {
        if(currentAttackType is AttackType.fourthAttack)
        {
            return;
        }

        StartExplosion(transform, _currentPower * (1 + _player.PassiveAtk_Power / 100));
    }

    private void StartExplosion(Transform transform, float currentPassivePower)
    {
        _forward = transform.forward;

        Vector3 explosionPosition = transform.position + transform.TransformDirection(_exploPosition) + _forward;

        OnExplosionParticle(explosionPosition);

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _data.Chip_AttackArea, _targetLayer);

        foreach(var target in  colliders)
        {
            IStatusEffect statusEffect = target.GetComponent<IStatusEffect>(); 

            if(statusEffect != null)
            {
                _explosionAction += (float passivePower) => statusEffect.Explosion(passivePower);
            }
        }

        _explosionAction?.Invoke(currentPassivePower);

        _explosionAction = null;
    }

    private void OnExplosionParticle(Vector3 explosionPosition)
    {
        GameObject explosionObject = _poolManager.DequeueObject(_effectObject);

        explosionObject.transform.position = explosionPosition;

        ParticleSystem system = explosionObject.GetComponent<ParticleSystem>();

        _blueChipSystem.StartCoroutine(EndParticle(system, explosionObject));
    }

    private IEnumerator EndParticle(ParticleSystem system, GameObject particle)
    {
        yield return new WaitUntil(() =>
        {
            return system.time >= system.main.duration;
        });

        _poolManager.EnqueueObject(particle);
    }

}
