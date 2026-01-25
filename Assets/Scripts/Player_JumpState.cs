using UnityEngine;

public class Player_JumpState : Player_AiredState
{
    public Player_JumpState(Player player, StateMachine stateMachine, string animeBoolName) : base(player, stateMachine, animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        //Make the object goes up, increate yVelocity

        player.setVelocity(rb.linearVelocityX, player.jumpForce);
    }

    public override void Update()
    {
        base.Update();

        //if yVelocity goes down, character is falling. transfer to fallState

        if (rb.linearVelocity.y < 0)
        {
            stateMachine.ChangeState(player.fallState);
        }
    }
}
