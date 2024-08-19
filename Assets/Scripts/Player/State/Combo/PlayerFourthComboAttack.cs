using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject.SpaceFighter;

public class PlayerFourthComboAttack : PlayerComboAttack
{
    public PlayerFourthComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.fourthAttack, FourthAttack);
        player.StartCoroutine(GetCamera());
    }
    
    private float _maxDelayTime = 2f;
    private float _currentDelayTime;
    private float _maxGauge = 4f;
    private float _gauge;
    private int _chargeCount;
    private bool _isFillingGauge = true;

    private CinemachineVirtualCamera _virtualCamera;
    private float _maxView = 90f;
    private float _currentView;

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
            _animator.speed = 0.03f;
        }
        
        if (!_isFillingGauge)
        {
            Debug.Log(Mathf.Round(_currentDelayTime));

            if (!_inputSystem.IsAttack || _currentDelayTime >= _maxDelayTime)
            {
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

        base.StateExit();
    }

    private IEnumerator GaugeAmount()
    {
        _effect.Active_FourthEffect(_player.CurrentAmmo);

        int index = 0;

        _effect.ChangeColor(index);

        float _elapsedTime = Time.time;

        while (_isFillingGauge)
        {          
            yield return new WaitForSeconds(0.1f);

            if (Time.time - _elapsedTime >= 1)
            {
                index += index == 4 ? 0 : 1;

                _elapsedTime += 1;

                _chargeCount--;

                _effect.ChangeColor(index);
            }

            _gauge += 0.1f;

            if(_gauge >= _maxGauge)
            {
                _isFillingGauge = false;
                yield break;
            }
            else if(_chargeCount < 0 || !_inputSystem.IsAttack)
            {
                _animator.speed = 1f;

                _state.ChangeState(State.Idle);

                yield break;
            }           
        }
    }

    private void FourthAttack()
    {
        _forward = _player.transform.forward;

        _boxPosition = _player.transform.position + _player.transform.TransformDirection(_additionalPosition) + _forward;

        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapBox(_boxPosition, _boxSize /2f, _player.transform.rotation, _enemyLayer);

        foreach(var target in  colliders)
        {
            IHit hit = target.gameObject.GetComponent<IHit>();

            Vector3 directionToPlayer = (_player.transform.position - target.transform.position).normalized;
            Vector3 hitPosition = target.transform.position + directionToPlayer * 1f;

            if(hit != null)
            {
                hit.Hit(10f, 5f, _player.transform);

                GameObject hitEffect = _effect.GetHitEffect();
                ParticleSystem hitParticle = hitEffect.GetComponent<ParticleSystem>();

                hitEffect.transform.position = hitPosition;
                Quaternion lookRotation = Quaternion.LookRotation(_player.transform.position);
                hitEffect.transform.rotation = lookRotation;

                hitParticle.Play();
                _effect.ReturnHit(hitEffect);
            }
        }

    }

    

    IEnumerator GetCamera()
    {
        yield return new WaitForSeconds(2f);

        CinemachineBrain brain = _player.MainCamera.GetComponent<CinemachineBrain>();

        var cinemaChineObject = brain.ActiveVirtualCamera.VirtualCameraGameObject;

        _virtualCamera = cinemaChineObject.GetComponent<CinemachineVirtualCamera>();
    }
}
