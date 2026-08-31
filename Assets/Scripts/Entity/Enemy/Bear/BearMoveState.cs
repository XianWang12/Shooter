using UnityEngine;

public class BearMoveState : EnemyState
{
    private Enemy_Bear enemy;
    private Vector3 lastTarget;

    public BearMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Bear enemy) : base(enemyBase, stateMachine, animBoolName)
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

        if(stateTimer>12f)
            stateMachine.ChangeState(enemy.frenzyState);
    }
}