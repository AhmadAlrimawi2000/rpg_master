using UnityEngine;


//@ It is a base clase for all the states that i am going to use in the future.

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string stateName;

    public EntityState(Player player, StateMachine stateMachine, string stateName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.stateName = stateName;
    }

    public virtual void Enter()
    {
        //Everytime state will be changed, Enter() will be called. 
        Debug.Log("I enter " + this.stateName);
    }

    public virtual void Update()
    {
        // The logic of the state is going to be run here.
        Debug.Log("I run update of the " + this.stateName);
    }

    public virtual void Exit()
    {
        //This will be called everytime we exit a state and change to a new one.
        Debug.Log("I exit " + this.stateName);
    }
}
