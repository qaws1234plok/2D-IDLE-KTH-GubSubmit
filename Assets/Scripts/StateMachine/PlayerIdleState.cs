using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerIdleState : PlayerNoneState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (EnemyManager.EnemyCount == 0)
        {
            stateMachine.ChangeState(stateMachine.findState);
        }

        if (EnemyManager.EnemyCount != 0 && AuotManager.Instance.IsAutoModeOn)
        {
            stateMachine.ChangeState(stateMachine.autoAttackState);
            SetAttackFalse();
        }
    }
    protected override void OnClickCanceled(InputAction.CallbackContext context)
    {
        base.OnClickCanceled(context);
        ExecuteAttack();
    }

    private void ExecuteAttack()
    {
        stateMachine.Player.Animator.SetBool("Attack", true);
    }

    public void SetAttackFalse()
    {
        stateMachine.Player.Animator.SetBool("Attack", false);
    }
}

