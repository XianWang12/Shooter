using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;

    public bool isInvincible;

    public float critChance;

    protected override void Start()
    {
        player = GetComponent<Player>();
        base.Start();
    }

    public override void DoDamage(CharacterStats target)
    {
        float totalDamage = damage;
        if (Random.value < critChance)
        {
            totalDamage *= 2;
        }
        target.TakeDamage(totalDamage);
    }

    public override void TakeDamage(float damage)
    {
        if (isInvincible)
            return;

        base.TakeDamage(damage);
        AudioManager.instance.PlaySFX(1);
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
        AudioManager.instance.PlaySFX(2);
    }
}
