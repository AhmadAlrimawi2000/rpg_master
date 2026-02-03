using UnityEngine;

public class Player_DashState : EntityState
{

    private float originalGravityScale;
    private int dashDirection;

    public Player_DashState(Player player, StateMachine stateMachine, string animeBoolName) : base(player, stateMachine, animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        dashDirection = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDirection;
        stateTimer = player.dashDuration;

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
    }


    public override void Update()
    {
        base.Update();

        CancelDashIfNeeded();

        player.setVelocity(player.dashSpeed * dashDirection, 0);

        if (stateTimer < 0)
        {
            if (player.isGrounded)
                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.fallState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.setVelocity(0, 0);
        rb.gravityScale = originalGravityScale;
    }

    private void CancelDashIfNeeded()
    {
        if (player.wallDetected)
        {
            if (player.isGrounded)

                stateMachine.ChangeState(player.idleState);
            else
                stateMachine.ChangeState(player.wallSlideState);
        }
    }

}

