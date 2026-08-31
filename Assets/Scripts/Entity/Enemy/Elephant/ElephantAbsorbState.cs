using UnityEngine;

public class ElephantAbsorbState : EnemyState
{
    Enemy_Elephant enemy;

    public ElephantAbsorbState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Elephant enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.isStopped = true;
        enemy.absorbVfx.Play();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.agent.isStopped = false;
        enemy.absorbVfx.Stop();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > enemy.absorbDuration)
        {
            stateMachine.ChangeState(enemy.summonState);
        }

        var target = Bait.Current ?? enemy.player.transform;

        if (!enemy.IsTargetInAbsorbZone(target))
            return;
        
        AbsorbTarget(target);
    }

    private void AbsorbTarget(Transform target)
    {
        var targetPlayer = target.GetComponent<Player>();
        if (targetPlayer == null)
            return;

        Vector3 dir = enemy.transform.position - target.position;
        dir.y = 0;
        dir.Normalize();

        Vector3 absorbForce=dir* enemy.absorbForce;

        targetPlayer.rb.AddForce(absorbForce, ForceMode.Acceleration);
    }
}