using System.Collections.Generic;
using UnityEngine;

public class Bait_Skill_Controller : MonoBehaviour
{
    private float damage;
    private float duration;
    private float blastRadius;
    private float moveSpeed;

    [SerializeField] private GameObject explosionPrefab;

    private float timer;

    private void Start()
    {
        timer = 0f;
    }

    public void SetUpClone(float baitDuration, float baitDamage, float baitBlastRadius, float baitMoveSpeed)
    {
        damage = baitDamage;
        duration = baitDuration;
        moveSpeed = baitMoveSpeed;
        blastRadius = baitBlastRadius;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(2, 2, 2), timer / duration);

        if (timer >= duration)
        {
            PlayerFX();
            Explode();
        }
    }

    private void PlayerFX()
    {
        GameObject vfx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);
        AudioManager.instance.PlaySFX(9);
    }

    private void Explode()
    {
        Destroy(gameObject);

        HashSet<Enemy> hitEnemies = new HashSet<Enemy>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider hit in hitColliders)
        {

            if (hit.CompareTag("Enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (!hitEnemies.Contains(enemy))
                {
                    enemy.stats.TakeDamage(damage);
                    enemy.PlayHitFX(enemy.transform.position, enemy.transform.forward);
                    hitEnemies.Add(enemy);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
