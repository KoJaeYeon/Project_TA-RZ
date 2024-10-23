using System.Collections;
using UnityEngine;

public class PlayerFirstComboAttack : PlayerComboAttack
{
    public PlayerFirstComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.firstAttack, FirstAttack);

        player.StartCoroutine(LoadData("A201"));

        _boxSize *= _player.PlayerAttackRange[0];
    }

    #region Overlap
    public Vector3 _forward { get; private set; }
    public Vector3 _boxPosition { get; private set; }   
    public Vector3 _boxSize { get; private set; } = new Vector3(1f, 1f, 2f);
    private Vector3 _additionalPosition = new Vector3(0f, 1f, 0.5f);
    private LayerMask _enemyLayer;
    public float _attackRange_Multiplier = 1f;
    #endregion

    public override void StateEnter()
    {
        base.StateEnter();

        _player.IsNext = false;

        _attackRange_Multiplier = _player.CurrentLevel != 4 ? 1f : 2f;

        ComboAnimation(_firstCombo, true);
    }

    public override void StateUpdate()
    {
        base.StateUpdate(); 

        OnComboAttackUpdate("Attack1", State.SecondComboAttack);
    }

    public override void StateExit()
    {
        ComboAnimation(_firstCombo, false);
    }

    protected override void ChangeData(int currentLevel)
    {
        base.ChangeData(currentLevel);
    }

    //첫 번째 공격로직
    private void FirstAttack()
    {
        _forward = _player.transform.forward;

        _boxPosition = _player.transform.position + _player.transform.TransformDirection(_additionalPosition) + _forward;        

        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapBox(_boxPosition, _boxSize / 2f * _attackRange_Multiplier, _player.transform.rotation, _enemyLayer);

        bool isHit = false;
        foreach(var target in  colliders)
        {
            IHit hit = target.gameObject.GetComponent<IHit>();

            Vector3 directionToPlayer = (_player.transform.position - target.transform.position).normalized;
            Vector3 hitPosition = target.transform.position + directionToPlayer * 1f + Vector3.up;

            if (hit != null)
            {
                ChangeData(_player.CurrentLevel);
                float damage = _player.CurrentAtk * _currentAtkMultiplier * _player._PC_Level.Level_Atk_Power_Multiplier;
                hit.Hit(damage, _currentStiffT, _player.transform);
                isHit = true;
                GameObject hitEffect = _effect.GetHitEffect();
                ParticleSystem hitParticle = hitEffect.GetComponent<ParticleSystem>();

                hitEffect.transform.position = hitPosition;
                Quaternion lookRotation = Quaternion.LookRotation(_player.transform.forward);
                hitEffect.transform.rotation = lookRotation;

                hitParticle.Play();
                _effect.ReturnHit(hitEffect);

                PrintDamageText(damage, target.transform);
            }
        }
        if (isHit)
        {
            _player.BlueChipSystem.UseBlueChip(_player.transform, AttackType.firstAttack);
            _player.CurrentSkill += _currentGetSkillGauge;
        }

        _player.CurrentAmmo -= _player.IsSkillAcitve[1] ? 0 : _player._PC_Level.Level_Consumption;
    }

}
