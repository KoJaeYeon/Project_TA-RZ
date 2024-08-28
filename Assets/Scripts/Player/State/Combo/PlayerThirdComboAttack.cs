using UnityEngine;

public class PlayerThirdComboAttack : PlayerComboAttack
{
    public PlayerThirdComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.thirdAttack, ThirdAttack);

        player.StartCoroutine(LoadData("A203"));
        rangeMultiplier *= _player.PlayerAttackRange[2];
    }

    #region Overlap
    public float _range { get; private set; } = 5f;
    public float _angle { get; private set; } = 120f;
    public float _height { get; private set; } = 5f;
    public float _segments { get; private set; } = 10f;
    private LayerMask _enemyLayer;
    private float rangeMultiplier = 1;
    #endregion

    public override void StateEnter()
    {
        base.StateEnter();  

        _player.IsNext = false;

        _range = _player.CurrentLevel != 4 ? 5f * rangeMultiplier : 10f * rangeMultiplier;

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
    }

    protected override void ChangeData(int currentLevel)
    {
        base.ChangeData(currentLevel);
    }

    //세 번째 공격로직
    private void ThirdAttack()
    {
        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapSphere(_player.transform.position, _range, _enemyLayer);

        bool isHit = false;
        foreach(var target in colliders)
        {
            if (IsRange(target.transform))
            {
                isHit = true;
                Hit(target);
            }
        }
        if (isHit)
        {
            _player.CurrentSkill += _currentGetSkillGauge;
        }

        _player.CurrentAmmo -= _player.IsSkillAcitve[1] ? 0 : _player._PC_Level.Level_Consumption;

    }

    //부채꼴 판정
    private bool IsRange(Transform targetTransform)
    {
        Vector3 targetDirection = targetTransform.position - _player.transform.position;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        Vector3 playerForward = _player.transform.forward;
        playerForward.y = 0f;
        playerForward.Normalize();

        float angleTotarget = Vector3.Angle(playerForward, targetDirection);
        if(angleTotarget > _angle / 2)
        {
            return false;
        }

        float distanceTotarget = Vector3.Distance(_player.transform.position, targetTransform.position);
        if(distanceTotarget > _range)
        {
            return false;
        }

        return true;
    }

    private void Hit(Collider other)
    {
        IHit hit = other.gameObject.GetComponent<IHit>();

        Vector3 directionToPlayer = (_player.transform.position - other.transform.position).normalized;

        Vector3 hitPosition = other.transform.position + directionToPlayer * 1f + Vector3.up;

        if (hit != null)
        {
            ChangeData(_player.CurrentLevel);
            float damage = _player.CurrentAtk * _currentAtkMultiplier * _player._PC_Level.Level_Atk_Power_Multiplier;
            hit.Hit(damage, _currentStiffT, _player.transform);
            hit.ApplyKnockback(_currentStiffT, _player.transform);
            
            GameObject hitEffect = _effect.GetHitEffect();
            ParticleSystem hitParticle = hitEffect.GetComponent<ParticleSystem>();

            hitEffect.transform.position = hitPosition;
            Quaternion lookRotation = Quaternion.LookRotation(_player.transform.forward);
            hitEffect.transform.rotation = lookRotation;

            hitParticle.Play();
            _effect.ReturnHit(hitEffect);

            PrintDamageText(damage, other.transform);
        }
    }
}
