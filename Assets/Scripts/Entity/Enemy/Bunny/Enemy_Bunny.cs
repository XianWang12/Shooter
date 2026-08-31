using UnityEngine;

public class Enemy_Bunny : Enemy
{
    [Header("Move info")]
    public float moveSpeed;
    public float dashSpeed;
    public float dashRange;

    public BunnyIdleState idleState { get; private set; }
    public BunnyMoveState moveState { get; private set; }
    public BunnyDashState dashState { get; private set; }
    public BunnyDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new BunnyIdleState(this, stateMachine, "Idle", this);
        moveState = new BunnyMoveState(this, stateMachine, "Move", this);
        dashState = new BunnyDashState(this, stateMachine, "Move", this);
        deadState = new BunnyDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);

        agent.speed = moveSpeed;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Update()
    {
        base.Update();

        if (player.stats.isDead)
            stateMachine.ChangeState(idleState);
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    protected override void ResetForPool()
    {
        base.ResetForPool();
        agent.speed = moveSpeed;
        stateMachine.Initialize(idleState);
    }

    public bool HasLineOfSight(Transform target)
    {
        var dirToTarget = target.position - transform.position;
        if (Physics.Raycast(transform.position + Vector3.up, dirToTarget.normalized, dashRange, groundLayer))
            return false;
        return true;
    }
}
