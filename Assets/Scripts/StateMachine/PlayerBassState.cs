using UnityEngine.InputSystem;

public class PlayerBassState : IState
{
    protected PlayerStateMachine stateMachine;

    public PlayerBassState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }
    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.playerActions.Click.canceled += OnClickCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.Player.Input;
        input.playerActions.Click.canceled -= OnClickCanceled;
    }

    protected virtual void OnClickCanceled(InputAction.CallbackContext context)
    {

    }

    public void HandleInput()
    {
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    public virtual void Update()
    {
    }
}
