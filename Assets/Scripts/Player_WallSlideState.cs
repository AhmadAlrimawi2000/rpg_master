using Unity.VisualScripting;
using UnityEngine;

public class Player_WallSlideState : EntityState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animeBoolName) : base(player, stateMachine, animeBoolName)
    {
    }


    public override void Update()
    {
        base.Update();

        HandleWallSlide();


        if (input.Player.Jump.WasPressedThisFrame())
            stateMachine.ChangeState(player.wallJumpState);


        if (!player.wallDetected)
            stateMachine.ChangeState(player.fallState);

        if (player.isGrounded)
        {
            stateMachine.ChangeState(player.idleState);
            player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
            player.setVelocity(player.moveInput.x, rb.linearVelocity.y);
        else
            player.setVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideSlowMultiplier);

    }

}
