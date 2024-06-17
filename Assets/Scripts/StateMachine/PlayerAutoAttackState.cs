using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoAttackState : PlayerNoneState
{
    public PlayerAutoAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AutoAttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AutoAttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (EnemyManager.EnemyCount == 0)
        {
            stateMachine.ChangeState(stateMachine.findState);
        }
    }
}
