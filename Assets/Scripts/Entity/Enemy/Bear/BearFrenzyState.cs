using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearFrenzyState : EnemyState
{
    private Enemy_Bear enemy;
    private Vector3 lastTarget;

    public BearFrenzyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Bear enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.speed = enemy.frenziedSpeed;
        enemy.GetComponent<Biting>().enabled = false; 
        enemy.GetComponent<FrenziedBiting>().enabled = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.speed = enemy.moveSpeed;
        enemy.GetComponent<Biting>().enabled = true;
        enemy.GetComponent<FrenziedBiting>().enabled = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
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
    }
}
