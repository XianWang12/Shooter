using System.Collections.Generic;
using UnityEngine;

public class Landmine_Skill_Controller : MonoBehaviour
{
    private float damage;
    private float blastRadius;
    [SerializeField] private GameObject explosionPrefab;

    public void SetUpClone(float damage, float blastRadius)
    {
        this.damage = damage;
        this.blastRadius = blastRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HashSet<Enemy> hitEnemies = new HashSet<Enemy>();
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

            foreach (Collider hit in colliders)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null && !hitEnemies.Contains(enemy))
                {
                    enemy.stats.TakeDamage(damage);
                    enemy.PlayHitFX(enemy.transform.position, enemy.transform.forward);
                    hitEnemies.Add(enemy);
                }
            }

            PlayVFX();
            Destroy(gameObject);
        }
    }

    private void PlayVFX()
    {
        GameObject vfx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(vfx, 1f);
        AudioManager.instance.PlaySFX(9);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, blastRadius);
    }
}
