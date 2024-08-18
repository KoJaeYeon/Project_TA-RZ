using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirdComboAttack : PlayerComboAttack
{
    public PlayerThirdComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.thirdAttack, ThirdAttack);
    }

    #region Overlap
    public float _range { get; private set; } = 5f;
    public float _angle { get; private set; } = 60f;
    public float _height { get; private set; } = 5f;
    public float _segments { get; private set; } = 10f;
    private LayerMask _enemyLayer;
    #endregion

    public override void StateEnter()
    {
        _player.IsNext = false;

        ComboAnimation(_thirdCombo, true);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        OnComboAttackUpdate("Attack3", State.FourthComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation(_thirdCombo, false);

        base.StateExit();
    }

    private void ThirdAttack()
    {
        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapSphere(_player.transform.position, _range, _enemyLayer);

        foreach(var target in colliders)
        {
            if (IsRange(target.transform))
            {
                Hit(target);
            }
        }
    }

    private bool IsRange(Transform targetTransform)
    {
        float targetY = targetTransform.position.y;
        float bottom = _player.transform.position.y;
        float top = bottom + _height;
        Debug.Log($"Y축 비교 : {targetY < bottom ||  targetY > top}");
        if(targetY < bottom ||  targetY > top)
        {
            return false;
        }

        Vector3 targetDirection = targetTransform.position - _player.transform.position;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        Vector3 playerForward = _player.transform.forward;
        playerForward.y = 0f;
        playerForward.Normalize();

        float angleTotarget = Vector3.Angle(playerForward, targetDirection);
        Debug.Log($"각도 비교 {angleTotarget > _angle / 2}");
        if(angleTotarget > _angle / 2)
        {
            return false;
        }

        float distanceTotarget = Vector3.Distance(_player.transform.position, targetTransform.position);
        Debug.Log($"거리비교{Vector3.Distance(_player.transform.position, targetTransform.position)}");
        if(distanceTotarget > _range)
        {
            return false;
        }

        return true;
    }

    private void Hit(Collider other)
    {
        IHit hit = other.gameObject.GetComponent<IHit>();

        Vector3 hitPosition = other.ClosestPoint(_player.transform.position);

        if (hit != null)
        {
            hit.Hit(10f, 0f, _player.transform);

            GameObject hitEffect = _effect.GetHitEffect();
            ParticleSystem hitParticle = hitEffect.GetComponent<ParticleSystem>();

            hitEffect.transform.position = hitPosition;
            hitEffect.transform.rotation = _player.transform.rotation;

            hitParticle.Play();
            _effect.ReturnHit(hitEffect);
        }
    }


}
