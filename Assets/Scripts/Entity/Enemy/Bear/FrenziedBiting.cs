using UnityEngine;

public class FrenziedBiting : Biting
{
    private Enemy_Bear bear;
    private float bitingInterval;

    protected override void Awake()
    {
        base.Awake();
        bitingInterval = 1f;
        bear = GetComponent<Enemy_Bear>();
    }

    protected override void Update()
    {
        if (bear.stats.isDead)
            return;

        timer += Time.deltaTime;

        if (canBite && timer >= bitingInterval)
            Bite();
    }

    protected override void Bite()
    {
        if (bear.player.stats.isDead)
            return;

        timer = 0f;
        bear.stats.DoDamage(bear.player.stats);
        bear.stats.CauseBleedingTo(bear.player.stats);
    }
}
