using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SmokePool : MonoBehaviour
{
    [SerializeField] private ParticleSystem prefab;
    [SerializeField] private int defaultCapacity = 4;
    [SerializeField] private int maxSize = 21;

    private ObjectPool<ParticleSystem> pool;

    private void Awake()
    {
        pool = new ObjectPool<ParticleSystem>(
            Create,
            OnGet,
            OnRelease,
            OnDestroyItem,
            true,
            defaultCapacity,
            maxSize);

        var instance = pool.Get();
        pool.Release(instance);
    }

    #region 对象池生命周期
    private ParticleSystem Create()
    {
        var instance = Instantiate(prefab, transform);
        instance.gameObject.SetActive(false);
        return instance;
    }

    private void OnGet(ParticleSystem fx)
    {
        fx.gameObject.SetActive(true);
        fx.transform.SetParent(null);
    }

    private void OnRelease(ParticleSystem fx)
    {
        fx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        fx.gameObject.SetActive(false);
        fx.transform.SetParent(transform);
    }

    private static void OnDestroyItem(ParticleSystem fx)
    {
        Destroy(fx.gameObject);
    }
    #endregion

    public ParticleSystem Spawn(Vector3 position, Quaternion rotation)
    {
        var fx = pool.Get();
        fx.transform.SetPositionAndRotation(position, rotation);
        fx.Play();
        StartCoroutine(ReleaseAfter(fx));
        return fx;
    }

    private IEnumerator ReleaseAfter(ParticleSystem fx)
    {
        var main = fx.main;
        var lifetime = main.duration + main.startLifetime.constantMax;
        yield return new WaitForSeconds(lifetime);
        pool.Release(fx);
    }
}
