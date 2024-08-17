using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSecondComboAttack : PlayerComboAttack
{
    public PlayerSecondComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.secondAttack, SecondAttack);
    }

    #region Overlap
    public Vector3 _forward { get; private set; }
    public Vector3 _boxPosition { get; private set; }
    public Vector3 _boxSize { get; private set; } = new Vector3(1f, 1f, 2.5f);
    private Vector3 _additionalPosition = new Vector3(0f, 1f, 0.7f);
    private LayerMask _enemyLayer;
    #endregion

    public override void StateEnter()
    {
        _player.IsNext = false;

        ComboAnimation(_secondCombo, true);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        OnComboAttackUpdate("Attack2", State.ThirdComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation(_secondCombo, false);

        base.StateExit();
    }

    private void SecondAttack()
    {
        _forward = _player.transform.forward;

        _boxPosition = _player.transform.position + _player.transform.TransformDirection(_additionalPosition) + _forward;

        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapBox(_boxPosition, _boxSize / 2f, _player.transform.rotation, _enemyLayer);

        foreach (var target in colliders)
        {
            IHit hit = target.gameObject.GetComponent<IHit>();

            Debug.Log(target.gameObject.gameObject.name);

            if (hit != null)
            {
                hit.Hit(10f, 5f, _player.transform);
            }
        }
    }
}

