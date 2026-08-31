using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyDeadState : EnemyState
{
    private Enemy_Bunny enemy;
    public BunnyDeadState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Bunny enemy) : base(enemyBase, stateMachine, animBoolName)
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
