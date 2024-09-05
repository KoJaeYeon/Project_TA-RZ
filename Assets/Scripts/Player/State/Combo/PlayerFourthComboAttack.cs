using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFourthComboAttack : PlayerComboAttack
{
    public PlayerFourthComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.fourthAttack, FourthAttack);

        player.StartCoroutine(LoadData("A204"));
        _boxSize *= _player.PlayerAttackRange[3];
    }

    private PC_Attack[] _fourthComboData = new PC_Attack[5];

    public override IEnumerator LoadData(string idStr)
    {
        while (true)
        {
            var comboData = _player.dataManager.GetData(idStr) as PC_Attack;
            var delayData = _player.dataManager.GetData("A500") as PC_Melee;

            if (comboData == null || delayData == null)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    comboData =_player.dataManager.GetData($"A20{i+4}") as PC_Attack;
                    _fourthComboData[i] = comboData;
                }
                _nextTime = delayData.Atk4_NextChargeT;
                _maxTime = delayData.Atk4_ChargeMaxT;
                Debug.Log($"{_maxTime}, {_nextTime}");
                yield break;
            }
        }
    }

    private void GetLevelSkillGage(int level, int chargeLevel)
    {
        level = _isLevel4 ? 4 : level;

        if(level == 4)
        {
            _currentGetSkillGauge = 0;
            return;
        }

        int gageGet = _fourthComboData[chargeLevel].Arm_SkillGageGet[level];

        _currentGetSkillGauge = gageGet;
    }

    #region Attack
    private float _maxIndex;
    private float _nextTime = 1f;
    private float _maxTime = 6f;
    private float _currentTime;
    private bool _isCharge = true;
    private bool _isLevel4 = false;
    private bool _isDrain = false;
    private int _index;

    private float _currentRadius = 1f;
    private float _maxRadius = 2f;
    private float _drainSpeed = 2f;
    #endregion

    #region Overlap
    public Vector3 _forward { get; private set; }
    public Vector3 _boxPosition { get; private set; }
    public Vector3 _boxSize { get; private set; } = new Vector3(2f, 1.5f, 10f);
    public Vector3 _additionalPosition = new Vector3(0f, 1f, 1f);
    private LayerMask _enemyLayer;
    public float _attackRange_Multiplier = 1f;
    #endregion

    public override void StateEnter()
    {
        _attackRange_Multiplier = _player.CurrentLevel != 4 ? 1f : 2f;

        _maxIndex = _player.CurrentAmmo >= 5 ? 4 : _player.CurrentAmmo;

        if (_maxIndex == 4)
        {
            _maxTime += 2f;
        }

        if (_player.CurrentLevel == 4)
        {
            _isLevel4 = true;

            _maxIndex = 4;
        }
        else
        {
            _isLevel4 = false;
        }

        _currentTime = 0f;

        _isCharge = true;

        ComboAnimation(_fourthCombo, true);

        _player.StartCoroutine(StartCharge());
    }

    public override void StateUpdate()
    {
        _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (_animatorStateInfo.IsName("Attack_Legend_Anim") &&_animatorStateInfo.normalizedTime >= 0.3f && _isCharge)
        {
            _animator.speed = 0.03f;
        }

        base.StateUpdate();
    }

    public override void StateExit()
    {
        _effect.DeActive_FourthEffect();

        ComboAnimation(_fourthCombo, false);

        _inputSystem.SetAttack(false);

        _maxTime = 6f;

        _player.drainSystem.OnSetDrainArea(0.7f);

    }

    private IEnumerator StartCharge()
    {
        _effect.Active_FourthEffect(_player.CurrentAmmo);

        _index = 0;

        _effect.ChangeColor(_index);

        _player.cameraRoot.StartCameraMovement();

        if (_player.IsPlayerFourthAttackDrainAvailable)
        {
            _player.StartCoroutine(ChargeDrain());
        }

        float _elapsedTime = Time.time;

        while (_isCharge)
        {
            yield return new WaitForSeconds(0.1f);

            if(Time.time - _elapsedTime >= _nextTime)
            {
                _isDrain = true;

                bool isNotLevel4AndLowAmmo = _player.CurrentLevel != 4 
                    && _player.CurrentAmmo - 1 <= _index;

                if (!isNotLevel4AndLowAmmo)
                {
                    _index += _index == _maxIndex ? 0 : 1;

                    _elapsedTime += _nextTime;

                    _effect.ChangeColor(_index);
                }
            }

            _currentTime += 0.1f;

            if(_currentTime >= _maxTime)
            {
                _isCharge = false;
            }
            else if (!_inputSystem.IsAttack)
            {
                _isDrain = false;

                _isCharge = false;

                _player.cameraRoot.EndCameraMovement();

                _animator.speed = 1f;

                _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

                while ((_animatorStateInfo.IsName("Attack_Legend_Anim") && _animatorStateInfo.normalizedTime <= 0.8f))
                {
                    yield return null;
                }

                _state.ChangeState(State.Idle);

                yield break;
            }
        }

        _player.cameraRoot.EndCameraMovement();

        _animator.speed = 1f;

        while ((_animatorStateInfo.IsName("Attack_Legend_Anim") && _animatorStateInfo.normalizedTime <= 0.8f))
        {
            yield return null;
        }

        _state.ChangeState(State.Idle);
    }

    private IEnumerator ChargeDrain()
    {        
        if (!_isDrain)
        {
            yield return new WaitWhile(() => !_isDrain);
        }

        while (_isDrain)
        {
            _currentRadius = _maxRadius *_index * 1.2f;
         
            _player.drainSystem.OnSetDrainArea(_currentRadius);

            yield return new WaitForSeconds(0.1f);
        }
    }

    protected override void ChangeData(int currentLevel)
    {
        _currentAtkMultiplier = _fourthComboData[_index].Atk_Multiplier;
        _currentStiffT = _fourthComboData[_index].AbnStatus_Value;
        GetLevelSkillGage(currentLevel, _index);
    }

    private void FourthAttack()
    {
        _forward = _player.transform.forward;

        _boxPosition = _player.transform.position + _player.transform.TransformDirection(_additionalPosition) + _forward;

        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapBox(_boxPosition, _boxSize /2f * _attackRange_Multiplier, _player.transform.rotation, _enemyLayer);

        bool isHit = false;
        foreach(var target in  colliders)
        {
            IHit hit = target.gameObject.GetComponent<IHit>();

            Vector3 directionToPlayer = (_player.transform.position - target.transform.position).normalized;
            Vector3 hitPosition = target.transform.position + directionToPlayer * 1f + Vector3.up;

            if (hit != null)
            {
                ChangeData(_player.CurrentLevel);
                PC_Level hitLevel = _isLevel4 ? _player.dataManager.GetData("P505") as PC_Level : _player._PC_Level;
                float damage = _player.CurrentAtk * _currentAtkMultiplier * hitLevel.Level_Atk_Power_Multiplier;
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
            _player.CurrentSkill += _currentGetSkillGauge;
        }

        _player.CurrentAmmo -= _player.IsSkillAcitve[1] || _isLevel4 ? 0 : _index + 1;

        //최초 4타 업적
        _player.OnCalled_Achieve_Charged();
    }
}
