using UnityEngine;

public class Player_FallState : EntityState
{
    public Player_FallState(Player player, StateMachine stateMachine, string animeBoolName) : base(player, stateMachine, animeBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        // check if player detecting the ground below, if yes, go to IdleState
        if (player.isGrounded)
            stateMachine.ChangeState(player.idleState);
    }
}
