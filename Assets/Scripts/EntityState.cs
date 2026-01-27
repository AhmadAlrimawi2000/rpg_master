using UnityEngine;


//@ It is a base class for all the states that will be used.

public abstract class EntityState
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animeBoolName;

    protected Animator anim;
    protected Rigidbody2D rb;
    protected PlayerInputSet input;

    public EntityState(Player player, StateMachine stateMachine, string animeBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animeBoolName = animeBoolName;
        this.anim = this.player.anim;
        this.rb = this.player.rb;
        this.input = this.player.input;
    }

    public virtual void Enter()
    {
        //Everytime state will be changed, Enter() will be called.
        if (stateMachine.currentState == player.wallSlideState)
            Debug.Log("Boolean Name: " + animeBoolName);
        anim.SetBool(animeBoolName, true);
    }

    public virtual void Update()
    {
        // The logic of the state is going to be run here.
        // Debug.Log("I run update of the " + this.animeBoolName);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    public virtual void Exit()
    {
        //This will be called everytime we exit a state and change to a new one.
        anim.SetBool(animeBoolName, false);

    }
}
