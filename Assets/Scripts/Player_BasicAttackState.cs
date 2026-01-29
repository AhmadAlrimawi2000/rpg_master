using UnityEngine;

public class Player_BasicAttackState : EntityState
{


    private const int FirstComboIndex = 1; //@ we start the combo index with 1, this parameter is used in the animator
    private float attackVelocityTimer;
    private int comboIndex = 1;

    private int comboLimit = 3;
    private float lastTimeAttacked;


    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animeBoolName) : base(player, stateMachine, animeBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
            comboLimit = player.attackVelocity.Length;
    }


    public override void Enter()
    {
        base.Enter();

        ResetComboIndex();

        anim.SetInteger("basicAttackIndex", comboIndex);

        ApplyAttackVelocity();

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

    public override void Exit()
    {
        base.Exit();

        comboIndex++;

        lastTimeAttacked = Time.time;

    }


    private void HandleAttackVelocity()
    {
        attackVelocityTimer -= Time.deltaTime;

        if (attackVelocityTimer < 0)
            player.setVelocity(0, rb.linearVelocity.y);
    }

    private void ApplyAttackVelocity()
    {

        Vector2 attackVelocity = player.attackVelocity[comboIndex - 1];

        attackVelocityTimer = player.attackVelocityDuration;

        player.setVelocity(attackVelocity.x * player.facingDirection, attackVelocity.y);
    }


    private void ResetComboIndex()
    {

        // if time was long ago, we will reset the combo index
        if (Time.time > lastTimeAttacked + player.comboResetTime)
            comboIndex = FirstComboIndex;

        if (comboIndex > comboLimit)
            comboIndex = FirstComboIndex;

    }
}
