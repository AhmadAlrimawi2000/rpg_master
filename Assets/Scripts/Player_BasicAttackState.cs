using UnityEngine;

public class Player_BasicAttackState : EntityState
{
    private float attackVelocityTimer;


    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animeBoolName) : base(player, stateMachine, animeBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        GenerateAttackVelocity();

    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();

        //@ detect and damage enemy


        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }


    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.setVelocity(0, rb.linearVelocity.y);
    }

    private void GenerateAttackVelocity()
    {
        attackVelocityTimer = player.attackVelocityDuration;

        player.setVelocity(player.attackVelocity.x * player.facingDirection, player.attackVelocity.y);
    }
}
