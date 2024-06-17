using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoneState : PlayerBassState
{
    public PlayerNoneState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.NoneParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.NoneParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
