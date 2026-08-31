using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.moveDir = new Vector3(xInput, 0, yInput).normalized;
        
        if (xInput == 0 && yInput == 0)
            stateMachine.ChangeState(player.idleState);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Move();
    }

    private void Move()
    {
        Vector3 moveVelocity = player.moveDir * player.moveSpeed * Time.fixedDeltaTime;
        player.rb.MovePosition(player.rb.position + moveVelocity);
    }
}
