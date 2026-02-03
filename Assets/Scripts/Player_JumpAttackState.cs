using UnityEngine;

public class Player_JumpAttackState : EntityState
{
    private bool touchedGround;

    public Player_JumpAttackState(Player player, StateMachine stateMachine, string animeBoolName) : base(player, stateMachine, animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        touchedGround = false;

        player.setVelocity(player.jumpAttackVelocity.x * player.facingDirection, player.jumpAttackVelocity.y);
    }

    public override void Update()
    {
        base.Update();

        if (player.isGrounded && !touchedGround)
        {
            touchedGround = true;
            anim.SetTrigger("jumpAttackTrigger");
            player.setVelocity(0, rb.linearVelocity.y);
        }

        if (triggerCalled && player.isGrounded)
            stateMachine.ChangeState(player.idleState);
    }
}
