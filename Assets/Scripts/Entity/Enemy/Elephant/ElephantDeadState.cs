using UnityEngine;

public class ElephantDeadState: EnemyState
{
    Enemy_Elephant enemy;
    public ElephantDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Elephant enemy) : base(enemyBase, stateMachine, animBoolName)
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