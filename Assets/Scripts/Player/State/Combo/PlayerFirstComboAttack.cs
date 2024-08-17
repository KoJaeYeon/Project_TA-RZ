using UnityEngine;

public class PlayerFirstComboAttack : PlayerComboAttack
{
    public PlayerFirstComboAttack(Player player) : base(player)
    {
        PlayerAnimationEvent _event;
        _event = player.GetComponentInChildren<PlayerAnimationEvent>();
        _event.AddEvent(AttackType.firstAttack, FirstAttack);
    }

    #region Overlap
    public Vector3 _forward { get; private set; }
    public Vector3 _boxPosition { get; private set; }   
    public Vector3 _boxSize { get; private set; } = new Vector3(1f, 1f, 2f);
    private Vector3 _additionalPosition = new Vector3(0f, 1f, 0.5f);
    private LayerMask _enemyLayer;
    #endregion

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

    private void FirstAttack()
    {
        _forward = _player.transform.forward;

        _boxPosition = _player.transform.position + _player.transform.TransformDirection(_additionalPosition) + _forward;

        _enemyLayer = LayerMask.GetMask("Monster");

        Collider[] colliders = Physics.OverlapBox(_boxPosition, _boxSize / 2f, _player.transform.rotation, _enemyLayer);

        foreach(var target in  colliders)
        {
            IHit hit = target.gameObject.GetComponent<IHit>();

            Vector3 hitPosition = target.ClosestPoint(_boxPosition);

            Debug.Log(target.gameObject.gameObject.name);

            if(hit != null)
            {
                hit.Hit(10f, 5f, _player.transform);

                GameObject hitEffect = _effect.GetHitEffect();
                ParticleSystem hitParticle = hitEffect.GetComponent<ParticleSystem>();

                hitEffect.transform.position = hitPosition;
                hitEffect.transform.rotation = _player.transform.rotation;

                hitParticle.Play();
                _effect.ReturnHit(hitEffect);
            }
        }
    }

}
