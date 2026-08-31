using UnityEngine;

public class Turret_Skill_Controller : MonoBehaviour
{
    private float damage;
    private float detectRadius;
    private float attackInterval;
    private float duration;

    
    private float intervalTime;
    private float fireFXShowTime = .04f;
    private float rotateSpeed = 360f;
    private Transform target;
    private RaycastHit hit;
    private float timer;
    [SerializeField] private Transform firePos;

    private Light fireLight;
    private LineRenderer fireLineRenderer;
    private ParticleSystem fireParticle;
    private LayerMask enemyLayerMask;

    private void Start()
    {
        timer = 0f;
        intervalTime = 0f;

        enemyLayerMask = LayerMask.GetMask("Enemy");

        fireLight = GetComponent<Light>();
        fireParticle = GetComponent<ParticleSystem>();
        fireLineRenderer = GetComponent<LineRenderer>();
    }

    public void SetUpTurret(float duration, float damage, float detectRadius, float attackInterval)
    {
        this.duration = duration;
        this.damage = damage;
        this.detectRadius = detectRadius;
        this.attackInterval = attackInterval;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        timer += Time.deltaTime;
        intervalTime += Time.deltaTime;

        if (target != null)
        {
            Enemy currentEnemy = target.GetComponent<Enemy>();
            if (currentEnemy.stats.isDead)
                target = null;
        }

        if (target != null && IsEnemyInRange(target))
        {
            if (intervalTime >= attackInterval)
                Attack();
        }
        else
            FindClosestTarget();

        Rotate();

        if(intervalTime >= fireFXShowTime)
        {
            fireLight.enabled = false;
            fireLineRenderer.enabled = false;
        }

        if (timer >= duration)
            DestorySelf();
    }

    private void Rotate()
    {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        if (dir.sqrMagnitude < 0.001f)
            return;

        Quaternion targetDir = Quaternion.LookRotation(dir.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetDir, rotateSpeed * Time.deltaTime);
    }

    private void FindClosestTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius, enemyLayerMask);
        float closestDistance = Mathf.Infinity;
        Transform closestTarget = null;
        foreach (Collider hit in hitColliders)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy.stats.isDead)
                continue;

            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = hit.transform;
            }
        }
        target = closestTarget;
    }

    private bool IsEnemyInRange(Transform target) => Vector3.Distance(transform.position, target.position) <= detectRadius;

    private void Attack()
    {
        intervalTime = 0f;

        AudioManager.instance.PlaySFX(0);

        fireLight.enabled = true;

        fireLineRenderer.SetPosition(0, firePos.position);
        fireLineRenderer.enabled = true;

        fireParticle.Play();

        Ray ray = new Ray(firePos.position, firePos.forward);
        if (Physics.Raycast(ray, out hit))
        {
            fireLineRenderer.SetPosition(1, hit.point);

            if (!hit.collider.CompareTag("Enemy"))
                return;

            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (!enemy.stats.isDead)
            {
                enemy.stats.TakeDamage(damage);
                enemy.PlayHitFX(hit.point + Vector3.up * 0.4f, hit.normal);
            }
        }
    }

    private void DestorySelf()
    {
        Destroy(transform.parent.gameObject, 2f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}