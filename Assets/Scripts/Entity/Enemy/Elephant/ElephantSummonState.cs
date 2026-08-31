using UnityEngine;

public class ElephantSummonState : EnemyState
{
    private Enemy_Elephant enemy;
    public ElephantSummonState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,Enemy_Elephant enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.isStopped = false;
        enemy.PlaySummonSmokeAndSpawn();

    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer>4f)
            stateMachine.ChangeState(enemy.idleState);
    }
}
