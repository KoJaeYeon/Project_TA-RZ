using Cinemachine;
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
    }

    private PC_Attack[] _fourthComboData = new PC_Attack[5];

    public override IEnumerator LoadData(string idStr)
    {
        while (true)
        {
            var comboData = _player.dataManager.GetData(idStr) as PC_Attack;

            if (comboData == null)
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
                yield break;
            }
        }
    }

   

    private void GetLevelSkillGage(int level, int chargeLevel)
    {
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
    private float _maxDelayTime = 2f;
    private float _currentDelayTime;
    private float _maxGauge = 4f;
    private float _gauge;
    private int _chargeCount;
    private bool _isFillingGauge = true;
    private int index = 0;
    #endregion

    #region Overlap
    public Vector3 _forward { get; private set; }
    public Vector3 _boxPosition { get; private set; }
    public Vector3 _boxSize { get; private set; } = new Vector3(2f, 1.5f, 10f);
    public Vector3 _additionalPosition = new Vector3(0f, 1f, 1f);
    private LayerMask _enemyLayer;
    #endregion

    public override void StateEnter()
    {
        _chargeCount = _player.CurrentAmmo - 1;

        _maxIndex = _player.CurrentLevel == 4 ? 4 : _chargeCount;

        if(_chargeCount >= 4)
        {
            _maxDelayTime += 2f;
        }

        _currentDelayTime = 0f;

        _gauge = 0f;

        _isFillingGauge = true;

        ComboAnimation(_fourthCombo, true);

        _player.StartCoroutine(GaugeAmount());
    }

    public override void StateUpdate()
    {
        _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        if (_animatorStateInfo.IsName("Attack_Legend_Anim") &&_animatorStateInfo.normalizedTime >= 0.3f)
        {
            _animator.speed = 0.03f;
        }
        
        if (!_isFillingGauge)
        {
            if (!_inputSystem.IsAttack || _currentDelayTime >= _maxDelayTime)
            {
                _player.cameraRoot.EndCameraMovement();

                _animator.speed = 1f;

                _state.ChangeState(State.Idle);

                return;
            }

            _currentDelayTime += Time.deltaTime;
        }
    }

    public override void StateExit()
    {
        _effect.DeActive_FourthEffect();

        ComboAnimation(_fourthCombo, false);

        _inputSystem.SetAttack(false);

        _maxDelayTime = 2f;

        _player.CurrentAmmo -= _player.IsSkillAcitve[1] ? 0 : index + 1;
    }

    private IEnumerator GaugeAmount()
    {
        _effect.Active_FourthEffect(_player.CurrentAmmo);

        index = 0;

        _effect.ChangeColor(index);

        _player.cameraRoot.StartCameraMovement();

        float _elapsedTime = Time.time;

        while (_isFillingGauge)
        {          
            yield return new WaitForSeconds(0.1f);

            if (Time.time - _elapsedTime >= 1)
            {
                if(_player.CurrentLevel != 4 && _player.CurrentAmmo - 1 <= index)
                {

                }
                else
                {
                    index += index == _maxIndex ? 0 : 1;

                    _elapsedTime += 1;

                    _chargeCount--;

                    _effect.ChangeColor(index);
                }
            }
            
            _gauge += 0.1f;

            if(_gauge >= _maxGauge)
            {
                _isFillingGauge = false;
                yield break;
            }
            else if(!_inputSystem.IsAttack)
            {
                _player.cameraRoot.EndCameraMovement();

                _animator.speed = 1f;

                _state.ChangeState(State.Idle);

                yield break;
            }           
        }
    }

    protected override void ChangeData(int currentLevel)
    {
        _currentAtkMultiplier = _fourthComboData[index].Atk_Multiplier;
        _currentStiffT = _fourthComboData[index].Atk4_StiffT;
        GetLevelSkillGage(currentLevel, index);
    }

    private void FourthAttack()
    {
        _forward = _player.transform.forward;

        _boxPosition = _player.transform.position + _player.transform.TransformDirection(_additionalPosition) + _forward;

        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapBox(_boxPosition, _boxSize /2f, _player.transform.rotation, _enemyLayer);

        bool isHit = false;
        foreach(var target in  colliders)
        {
            IHit hit = target.gameObject.GetComponent<IHit>();

            Vector3 directionToPlayer = (_player.transform.position - target.transform.position).normalized;
            Vector3 hitPosition = target.transform.position + directionToPlayer * 1f + Vector3.up;

            if(hit != null)
            {
                ChangeData(_player.CurrentLevel);
                hit.Hit(_player.CurrentAtk * _currentAtkMultiplier * _player._PC_Level.Level_Atk_Power_Multiplier, _currentStiffT, _player.transform);
                isHit = true;
                GameObject hitEffect = _effect.GetHitEffect();
                ParticleSystem hitParticle = hitEffect.GetComponent<ParticleSystem>();

                hitEffect.transform.position = hitPosition;
                Quaternion lookRotation = Quaternion.LookRotation(_player.transform.position);
                hitEffect.transform.rotation = lookRotation;

                hitParticle.Play();
                _effect.ReturnHit(hitEffect);
            }
        }
        if (isHit)
        {
            _player.CurrentSkill += _currentGetSkillGauge;
        }

    }
}
