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
    }

    private PC_Attack _attackData;

    private IEnumerator LoadData(string idStr)
    {
        while (true)
        {
            var firstComboData = _player.dataManager.GetData(idStr) as PC_Attack;
            if(firstComboData == null )
            {
                Debug.Log("1번 콤보 데이터를 가져오지 못했습니다.");
                yield return new WaitForSeconds(1f);
            }
            else
            {
                _attackData = firstComboData;
                PC_Type1_Atk_Multiplier = _attackData.Atk_Multiplier;
                
                Debug.Log("1번 콤보 데이터를 성공적으로 가져왔습니다.");
                yield break;
            }

        }
    }

    #region Overlap
    public Vector3 _forward { get; private set; }
    public Vector3 _boxPosition { get; private set; }   
    public Vector3 _boxSize { get; private set; } = new Vector3(1f, 1f, 2f);
    private Vector3 _additionalPosition = new Vector3(0f, 1f, 0.5f);
    private LayerMask _enemyLayer;
    #endregion

    private float _currentAtk;
    private float PC_Type1_Atk_Multiplier;

    public override void StateEnter()
    {
        _player.IsNext = false;

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

        base.StateExit();
    }

    //첫 번째 공격로직
    private void FirstAttack()
    {
        _forward = _player.transform.forward;

        _boxPosition = _player.transform.position + _player.transform.TransformDirection(_additionalPosition) + _forward;

        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapBox(_boxPosition, _boxSize / 2f, _player.transform.rotation, _enemyLayer);

        foreach(var target in  colliders)
        {
            IHit hit = target.gameObject.GetComponent<IHit>();

            Vector3 directionToPlayer = (_player.transform.position - target.transform.position).normalized;
            Vector3 hitPosition = target.transform.position + directionToPlayer * 1f;

            if (hit != null)
            {
                _currentAtk = _player.CurrentAtk * PC_Type1_Atk_Multiplier;

                hit.Hit(_currentAtk, 5f, _player.transform);

                GameObject hitEffect = _effect.GetHitEffect();
                ParticleSystem hitParticle = hitEffect.GetComponent<ParticleSystem>();

                hitEffect.transform.position = hitPosition;
                Quaternion lookRotation = Quaternion.LookRotation(_player.transform.forward);
                hitEffect.transform.rotation = lookRotation;

                hitParticle.Play();
                _effect.ReturnHit(hitEffect);
            }
        }
    }

}
