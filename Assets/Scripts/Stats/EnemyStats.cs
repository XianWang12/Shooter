public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    protected override void Start()
    {
        enemy = GetComponent<Enemy>();
        base.Start();
    }

    override public void DoDamage(CharacterStats target)
    {
        target.TakeDamage(damage);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        int sfx = GetSoundForEnemy(enemy)[0];
        AudioManager.instance.PlaySFX(sfx);
    }

    protected override void Die()
    {
        base.Die();
        if (ScoreManager.instance != null)
        {
            int score = ScoreManager.GetScoreForEnemy(enemy);
            ScoreManager.instance.AddScore(score);
        }

        int sfx = GetSoundForEnemy(enemy)[1];
        AudioManager.instance.PlaySFX(sfx);

        enemy.Die();
    }

    private int[] GetSoundForEnemy(Enemy enemy)
    {
        if (enemy is Enemy_Elephant)
            return new int[] { 3, 4 };
        if (enemy is Enemy_Bear)
            return new int[] { 5, 6 };
        else
            return new int[] { 7, 8 };
    }
}
