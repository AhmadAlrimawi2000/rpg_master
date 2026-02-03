using UnityEngine;

public class Player_BasicAttackState : EntityState
{


    private const int FirstComboIndex = 1; //@ we start the combo index with 1, this parameter is used in the animator
    private float attackVelocityTimer;
    private float lastTimeAttacked;
    private bool comboAttackQueued;
    private int attackDirection;
    private int comboIndex = 1;
    private int comboLimit = 3;


    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animeBoolName) : base(player, stateMachine, animeBoolName)
    {
        if (comboLimit != player.attackVelocity.Length)
        {
            comboLimit = player.attackVelocity.Length;
            Debug.LogWarning("Adjusted combo limit to match attack velocity array!");
        }
    }


    public override void Enter()
    {
        base.Enter();
        comboAttackQueued = false;

        ResetComboIndex();

        //@ Define attack direction according to the input
        attackDirection = player.moveInput.x != 0 ? ((int)player.moveInput.x) : player.facingDirection;

        anim.SetInteger("basicAttackIndex", comboIndex);

        ApplyAttackVelocity();

    }

    public override void Update()
    {
        base.Update();

        HandleAttackVelocity();


        //@ detect and damage enemy

        if (input.Player.Attack.WasPressedThisFrame())
            QueueNextAttack();


        if (triggerCalled)
            HandleStateExit();
    }

    public override void Exit()
    {
        base.Exit();

        comboIndex++;

        lastTimeAttacked = Time.time;

    }

    private void HandleStateExit()
    {
        if (comboAttackQueued)
        {
            anim.SetBool(animeBoolName, false);
            player.EnterAttackStateWithDelay();
        }

        else
            stateMachine.ChangeState(player.idleState);
    }

    private void QueueNextAttack()
    {
        if (comboIndex < comboLimit)
            comboAttackQueued = true;
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

        player.setVelocity(attackVelocity.x * attackDirection, attackVelocity.y);
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
