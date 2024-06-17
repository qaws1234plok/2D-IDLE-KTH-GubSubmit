using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public PlayerCharacter Player {  get; }

    public PlayerIdleState IdleState { get; }
    public PlayerFindState findState { get; }
    public PlayerAutoAttackState autoAttackState { get; }
    public PlayerStateMachine(PlayerCharacter player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        findState = new PlayerFindState(this);
        autoAttackState = new PlayerAutoAttackState(this);
    }
}
