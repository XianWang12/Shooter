using UnityEngine;

public class BearDeadState : EnemyState
{
    Enemy_Bear enemy;
    public BearDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Bear enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.agent.isStopped = true;
        enemy.agent.ResetPath();
        enemy.agent.velocity = Vector3.zero;
        enemy.rb.velocity = Vector3.zero;
        enemy.rb.angularVelocity = Vector3.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
    }
}