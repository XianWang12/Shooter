using System.Collections;
using UnityEngine;

public class Enemy_Elephant : Enemy
{
    [Header("Move info")]
    public float moveSpeed;

    [Header("Absorb info")]
    public float absorbForce;
    public float absorbAngle;
    public float absorbRadius;
    public float absorbDuration;

    [Header("Absorb VFX")]
    public ParticleSystem absorbVfx;

    [Header("Summon info")]
    public float summonSmokeRadius = 1.5f;
    public float summonSpawnRadius = 2.5f;

    public ElephantIdleState idleState { get; private set; }
    public ElephantMoveState moveState { get; private set; }
    public ElephantAbsorbState adsorbState { get; private set; }
    public ElephantSummonState summonState { get; private set; }
    public ElephantDeadState deadState { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        idleState = new ElephantIdleState(this, stateMachine, "Idle", this);
        moveState = new ElephantMoveState(this, stateMachine, "Move", this);
        adsorbState = new ElephantAbsorbState(this, stateMachine, "Absorb", this);
        summonState = new ElephantSummonState(this, stateMachine, "Idle", this);
        deadState = new ElephantDeadState(this, stateMachine, "Dead", this);
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

    #region ÕÙ»½×´Ì¬Ïà¹Ø
    public void PlaySummonSmokeAndSpawn()
    {
        var smokePosition = transform.position + Random.insideUnitSphere * summonSmokeRadius;
        smokePosition.y = transform.position.y;

        var smoke = SmokePool.Spawn(smokePosition, Quaternion.identity);

        StartCoroutine(SpawnAfterSmoke(smoke));
    }

    private IEnumerator SpawnAfterSmoke(ParticleSystem smoke)
    {
        var main = smoke.main;
        var lifetime = main.duration + main.startLifetime.constantMax;
        yield return new WaitForSeconds(lifetime);
        var spawnCenter = smoke.transform.position;
        SpawnSummonedEnemies(spawnCenter);
    }

    private void SpawnSummonedEnemies(Vector3 spawnCenter)
    {
        SpawnEnemyGroup(EnemyType.Bunny, 3, spawnCenter);
        SpawnEnemyGroup(EnemyType.Bear, 1, spawnCenter);

        if (Random.value <= 0.1f)
            SpawnEnemyGroup(EnemyType.Elephant, 1, spawnCenter);
    }

    private void SpawnEnemyGroup(EnemyType type, int count, Vector3 spawnCenter)
    {
        if (EnemyPool == null || player == null)
            return;

        for (int i = 0; i < count; i++)
        {
            var offset = Random.insideUnitCircle * summonSpawnRadius;
            var position = spawnCenter + new Vector3(offset.x, 0f, offset.y);
            EnemyPool.Spawn(type, position, transform.rotation, player);
        }
    }
    #endregion

    protected override void ResetForPool()
    {
        base.ResetForPool();
        agent.speed = moveSpeed;
        stateMachine.Initialize(idleState);
    }

    public bool IsPlayerInAbsorbZone()
    {
        return IsTargetInAbsorbZone(player == null ? null : player.transform);
    }

    public bool IsTargetInAbsorbZone(Transform target)
    {
        Vector3 dirToTarget = target.position - transform.position;
        float distanceToTarget = dirToTarget.magnitude;
        if (distanceToTarget > absorbRadius) return false;

        float angle = Vector3.Angle(transform.forward, dirToTarget);
        if (angle > absorbAngle / 2) return false;
        
        if (Physics.Raycast(transform.position + Vector3.up, dirToTarget.normalized, absorbRadius, groundLayer))
        {
            return false;
        }
        return true;
    }
}
