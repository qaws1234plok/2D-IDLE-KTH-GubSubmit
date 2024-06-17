using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFindState : PlayerNoneState
{
    public PlayerFindState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.FindParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.FindParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (EnemyManager.EnemyCount != 0)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
