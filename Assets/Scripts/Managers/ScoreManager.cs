using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int CurrentScore { get; private set; }

    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);
    }

    public void AddScore(int amount)
    {
        //if (amount <= 0)
        //    return;

        CurrentScore += amount;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public static int GetScoreForEnemy(Enemy enemy)
    {
        if (enemy == null)
            return 0;

        if (enemy is Enemy_Bunny)
            return 10;

        if (enemy is Enemy_Bear)
            return 15;

        if (enemy is Enemy_Elephant)
            return 25;

        return 0;
    }
}
