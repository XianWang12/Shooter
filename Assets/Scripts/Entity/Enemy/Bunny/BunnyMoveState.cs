using UnityEngine;

public class BunnyMoveState : EnemyState
{
    private Enemy_Bunny enemy;
    private Vector3 lastTarget;

    public BunnyMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Bunny enemy) : base(enemy, stateMachine, animBoolName)
    {
        this.enemy = enemy;
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
        var target = Bait.Current ?? enemy.player.transform;

        if (Vector3.Distance(lastTarget, target.position) > .1f)
        {
            enemy.agent.SetDestination(target.position);
            lastTarget = target.position;
        }

        if (Vector3.Distance(enemy.transform.position, target.position) < enemy.dashRange && enemy.HasLineOfSight(target) && stateTimer > 2f)
            stateMachine.ChangeState(enemy.dashState);
    }
}
