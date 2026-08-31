using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Player player;

    private Light fireLight;
    private LineRenderer fireLineRenderer;
    private ParticleSystem fireParticle;

    private float intervalTime = 0f;
    private float fireFXShowTime = .04f;
    private float timeBetweenBullets = 0.15f;

    private RaycastHit shootHit;
    public LayerMask shootMask;

    private void Awake()
    {
        player =PlayerManager.instance.player;
        fireLight = GetComponent<Light>();
        fireParticle = GetComponent<ParticleSystem>();
        fireLineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(Time.timeScale == 0)
            return;

        intervalTime += Time.deltaTime;

        if(player.stats.isDead)
            return;

        if (Input.GetButton("Fire1") && intervalTime >= timeBetweenBullets)
            Shoot();

        if (intervalTime > fireFXShowTime)
        {
            fireLight.enabled = false;
            fireLineRenderer.enabled = false;
        }
    }

    private void Shoot()
    {
        intervalTime = 0f;

        AudioManager.instance.PlaySFX(0);
        
        fireLight.enabled = true;

        fireLineRenderer.SetPosition(0, transform.position);
        fireLineRenderer.enabled = true;

        fireParticle.Play();

        if (Physics.Raycast(transform.position, transform.forward, out shootHit, 100, shootMask))
        { 
            fireLineRenderer.SetPosition(1, shootHit.point);
            if(shootHit.collider.CompareTag("Enemy"))
            {
                var enemyStats = shootHit.collider.GetComponent<EnemyStats>();
                var enemy = shootHit.collider.GetComponentInParent<Enemy>();

                if (!enemyStats.isDead)
                {
                    player.stats.DoDamage(enemyStats);
                    enemy.PlayHitFX(shootHit.point, shootHit.normal);
                }
            }
        } 
        else
            fireLineRenderer.SetPosition(1, transform.position + transform.forward * 100);
    }
}
