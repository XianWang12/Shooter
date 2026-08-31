using UnityEngine;

public class Biting : MonoBehaviour
{
    private Enemy enemy;

    protected bool canBite;

    protected float timer = 0f;
    private float timeBetweenBites = 2f;

    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    protected virtual void Update()
    {
        if (enemy.stats.isDead)
            return;

        timer += Time.deltaTime;

        if (canBite && timer >= timeBetweenBites)
            Bite();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !enemy.stats.isDead)
            canBite = true;
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canBite = false;
        }
    }

    protected virtual void Bite()
    {
        if (enemy.player.stats.isDead)
            return;

        timer = 0f;
        enemy.stats.DoDamage(enemy.player.stats);
    }
}
