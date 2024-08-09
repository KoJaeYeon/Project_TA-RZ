using UnityEngine;
using Zenject;

public class PlayerAttackStateBehaviour : StateMachineBehaviour
{
    [Inject]
    private Player _player;

    #region AnimatorStringToHash
    private readonly int _firstCombo = Animator.StringToHash("ComboAttack1");
    private readonly int _secondCombo = Animator.StringToHash("ComboAttack2");
    private readonly int _thirdCombo = Animator.StringToHash("ComboAttack3");
    private readonly int _fourthCombo = Animator.StringToHash("ComboAttack4");
    #endregion


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("ComboFail");
    }

}
