using UnityEngine;

public class Enemy_Bear : Enemy
{
    [Header("Move info")]
    public float moveSpeed;
    public float frenziedSpeed;

    public BearIdleState idleState { get; private set; }
    public BearMoveState moveState { get; private set; }
    public BearFrenzyState frenzyState { get; private set; }
    public BearDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new BearIdleState(this, stateMachine, "Idle", this);
        moveState = new BearMoveState(this, stateMachine, "Move", this);
        frenzyState = new BearFrenzyState(this, stateMachine, "Move", this);
        deadState = new BearDeadState(this, stateMachine, "Dead", this);
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
}
