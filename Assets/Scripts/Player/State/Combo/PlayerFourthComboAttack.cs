using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFourthComboAttack : PlayerComboAttack
{
    public PlayerFourthComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.fourthAttack, ChargeAttack);
    }
    
    private float _maxDelayTime = 2f;
    private float _currentDelayTime;
    private float _maxGauge = 4f;
    private float _gauge;
    private bool _isFillingGauge = true;
    
    public override void StateEnter()
    {
        _currentDelayTime = 0f;

        _gauge = 0f;

        _isFillingGauge = true;

        ComboAnimation(_fourthCombo, true);

        _player.StartCoroutine(GaugeAmount());
    }

    public override void StateUpdate()
    {
        _animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        //4초면 4초에 0.3f, 0.03f
        if (_animatorStateInfo.IsName("Attack_Legend_Anim") &&_animatorStateInfo.normalizedTime >= 0.3f)
        {
            _animator.speed = 0.05f;
        }
        
        if (!_isFillingGauge)
        {
            Debug.Log(Mathf.Round(_currentDelayTime));

            EndState();
        }
    }

    public override void StateExit()
    {
        _effect.DeActive_FourthEffect();
        ComboAnimation(_fourthCombo, false);

        _inputSystem.SetAttack(false);

        base.StateExit();
    }

    private IEnumerator GaugeAmount()
    {
        _effect.Active_FourthEffect();

        int index = 0;
        
        _effect.ChangeColor(index);

        while (_isFillingGauge)
        {
            yield return new WaitForSeconds(0.5f);
            index += index == 3 ? 0 : 1;
            _effect.ChangeColor(index);
            _gauge += 1f;

            Debug.Log(_gauge);

            if(_gauge >= _maxGauge)
            {
                _isFillingGauge = false;

                yield break;
            }
            else if (!_inputSystem.IsAttack)
            {
                Debug.Log("취소");

                _animator.speed = 1f;

                _state.ChangeState(State.Idle);

                yield break;
            }
        }
    }

    private void EndState()
    {
        if(!_inputSystem.IsAttack || _currentDelayTime >= _maxDelayTime)
        {
            _animator.speed = 1f;

            _state.ChangeState(State.Idle);

            return;
        }

        _currentDelayTime += Time.deltaTime;
    }

    private void ChargeAttack()
    {
        
    }



    private void FourthAttack(float time)
    {

    }
}
