using UnityEngine;


//@ Should have a reference to the current active state 
public class StateMachine
{
    public EntityState currentState { get; private set; }

    public void Initialize(EntityState startState)
    {
        this.currentState = startState;
        this.currentState.Enter();
    }

    public void ChangeState(EntityState newState)
    {
        this.currentState.Exit();
        this.currentState = newState;
        this.currentState.Enter();
    }

    public void UpdateActiveState()
    {
        currentState.Update();
    }
}
