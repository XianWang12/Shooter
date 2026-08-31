using UnityEngine;

public class BunnyDashState : EnemyState
{
    Enemy_Bunny enemy;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float dashDuration;
    public BunnyDashState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName,Enemy_Bunny enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        var target = Bait.Current ?? enemy.player.transform;

        Vector3 lookDir = target.position - enemy.transform.position;
        lookDir.y = 0; 
        enemy.transform.rotation = Quaternion.LookRotation(lookDir);

        enemy.agent.isStopped = true;

        startPosition = enemy.transform.position;
        endPosition = target.position;
        dashDuration = Vector3.Distance(startPosition, endPosition) / enemy.dashSpeed;
    }

    public override void Exit()
    {
        base.Exit();

        enemy.agent.isStopped = false;
        enemy.agent.Warp(enemy.transform.position);
    }

    public override void Update()
    {
        base.Update();
        enemy.transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.Clamp01(stateTimer/dashDuration));
    
        if(stateTimer>dashDuration)
            enemy.stateMachine.ChangeState(enemy.idleState);
    }
}
