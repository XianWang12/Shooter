using UnityEngine;

public class ElephantMoveState: EnemyState
{
    private Enemy_Elephant enemy;
    private Vector3 lastTarget;

    public ElephantMoveState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Elephant enemy) : base(enemyBase, stateMachine, animBoolName)
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

        if (Vector3.Distance(enemy.transform.position, target.position) < enemy.absorbRadius && enemy.IsTargetInAbsorbZone(target) && stateTimer > 10f)
        {
            stateMachine.ChangeState(enemy.adsorbState);
        }
    }
}