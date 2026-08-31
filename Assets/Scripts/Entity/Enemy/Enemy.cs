using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity, IPoolable
{
    private ItemDrop itemDrop;
    public EnemyStats stats { get; private set; }

    public Player player;
    public NavMeshAgent agent;

    private HitBloodPool hitBloodPool;
    private EnemyPool enemyPool;
    private SmokePool smokePool;

    public event Action<Enemy> OnDeath;

    public EnemyStateMachine stateMachine { get; private set; }
    private Coroutine despawnRoutine;

    protected override void Awake()
    {
        base.Awake();
        itemDrop = GetComponent<ItemDrop>();
        stats = GetComponent<EnemyStats>();
        stateMachine=new EnemyStateMachine();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        stateMachine.currentState.FixedUpdate();
    }

    # region 依赖注入
    public void SetPlayer(Player target)
    {
        player = target;
    }

    public void SetHitBloodPool(HitBloodPool pool)
    {
        hitBloodPool = pool;
    }

    public void SetEnemyPool(EnemyPool pool)
    {
        enemyPool = pool;
    }

    public void SetSmokePool(SmokePool pool)
    {
        smokePool = pool;
    }
    #endregion

    #region 受保护的属性访问器
    protected EnemyPool EnemyPool => enemyPool;

    protected SmokePool SmokePool => smokePool;
    #endregion

    public override void Die()
    {
        base.Die();
        itemDrop.DropItem();
        OnDeath?.Invoke(this);
        StartDespawnTimer();
    }

    #region  启动死亡后的回收计时
    private void StartDespawnTimer()
    {
        var pooled = GetComponent<PooledEnemy>();

        despawnRoutine = StartCoroutine(DespawnAfterDelay(pooled, 2f));
    }

    private IEnumerator DespawnAfterDelay(PooledEnemy pooled, float delay)
    {
        yield return new WaitForSeconds(delay);
        pooled.ReturnToPool();
        despawnRoutine = null;
    }
    #endregion

    # region 用于对象被取出/回收时做初始化和清理
    public virtual void OnSpawned()
    {
        if (despawnRoutine != null)
        {
            StopCoroutine(despawnRoutine);
            despawnRoutine = null;
        }

        ResetForPool();
    }

    protected virtual void ResetForPool()
    {
        stats.ResetStats();

        agent.isStopped = false;
        agent.ResetPath();
    }

    public virtual void OnDespawned()
    {

        agent.isStopped = true;
        agent.ResetPath();
        agent.velocity = Vector3.zero;


        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }
    #endregion

    public void PlayHitFX(Vector3 position, Vector3 normal) => hitBloodPool.Spawn(position, Quaternion.LookRotation(normal));
}
