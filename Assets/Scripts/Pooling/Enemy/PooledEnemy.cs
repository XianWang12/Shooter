using UnityEngine;
using UnityEngine.Pool;

public class PooledEnemy : MonoBehaviour
{
    private IObjectPool<Enemy> pool;

    public void Initialize(IObjectPool<Enemy> enemyPool)
    {
        pool = enemyPool;
    }

    public void ReturnToPool()
    {
        var enemy = GetComponent<Enemy>();
        pool.Release(enemy);
    }
}
