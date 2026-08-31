public class ElephantIdleState: EnemyState
{
    Enemy_Elephant enemy;

    public ElephantIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Elephant enemy) : base(enemyBase, stateMachine, animBoolName)
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

        if (enemy.player.stats.isDead)
            return;

        if (stateTimer > .5f)
            stateMachine.ChangeState(enemy.moveState);
    }
}