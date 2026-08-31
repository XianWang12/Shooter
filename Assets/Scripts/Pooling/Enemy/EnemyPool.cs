using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public enum EnemyType
{
    Bear,
    Bunny,
    Elephant
}

[System.Serializable]
public class EnemyPoolItem
{
    public EnemyType type;
    public Enemy prefab;
    public int initialSize = 5;
}

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<EnemyPoolItem> pools = new List<EnemyPoolItem>();
    [SerializeField] private HitBloodPool hitBloodPool;
    [SerializeField] private SmokePool smokePool;
    [SerializeField] private int maxSize = 50;

    private readonly Dictionary<EnemyType, ObjectPool<Enemy>> poolLookup = new Dictionary<EnemyType, ObjectPool<Enemy>>();
    
    private Transform poolRoot;

    private void Awake()
    {
        poolRoot = new GameObject("EnemyPool").transform;
        poolRoot.SetParent(transform);

        foreach (var item in pools)
        {
            if (poolLookup.ContainsKey(item.type))
                continue;

            ObjectPool<Enemy> pool = null;
            pool = new ObjectPool<Enemy>(
                () => Create(item, pool),
                OnGet,
                OnRelease,
                OnDestroy,
                true,
                item.initialSize,
                maxSize
                );

            poolLookup.Add(item.type, pool);

            var enemy = pool.Get();
            pool.Release(enemy);
        }
    }
    #region 对象池生命周期
    private Enemy Create(EnemyPoolItem item, IObjectPool<Enemy> pool)
    {
        var instance = Instantiate(item.prefab, poolRoot);
        instance.gameObject.SetActive(false);

        var pooled = instance.GetComponent<PooledEnemy>();
        if (pooled == null)
            pooled = instance.gameObject.AddComponent<PooledEnemy>();

        pooled.Initialize(pool);
        return instance;
    }

    private static void OnGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.transform.SetParent(null);
        var poolable = enemy.GetComponent<IPoolable>();
        poolable?.OnSpawned();
    }

    private void OnRelease(Enemy enemy)
    {
        var poolable = enemy.GetComponent<IPoolable>();
        poolable?.OnDespawned();
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(poolRoot);
    }

    private static void OnDestroy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
    #endregion

    public Enemy Spawn(EnemyType type, Vector3 position, Quaternion rotation, Player player)
    {
        if (!poolLookup.TryGetValue(type, out var pool))
            return null;

        Enemy instance = pool.Get();

        instance.transform.SetPositionAndRotation(position, rotation);

        instance.SetPlayer(player);
        instance.SetHitBloodPool(hitBloodPool);
        instance.SetEnemyPool(this);
        instance.SetSmokePool(smokePool);

        return instance;
    }
}
