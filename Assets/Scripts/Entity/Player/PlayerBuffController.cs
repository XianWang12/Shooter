using System.Collections;
using UnityEngine;

public class PlayerBuffController : MonoBehaviour
{
    private Player player;
    private PlayerStats stats;

    public bool speedPotionActive;
    private float speedPotionEndTime;
    private float defaultMoveSpeed;
    private Coroutine speedPotionCoroutine;

    public bool strengthPotionActive;
    private float strengthPotionEndTime;
    private float defaultCritChance;
    private Coroutine strengthPotionCoroutine;

    private void Awake()
    {
        player = GetComponent<Player>();
        stats = GetComponent<PlayerStats>();
    }

    public void ApplyOrExtendSpeedPotion(float addDurationSeconds, float speedMultiplier)
    {
        if (player == null)
            return;

        if (!speedPotionActive)
        {
            speedPotionActive = true;

            defaultMoveSpeed = player.moveSpeed;
            player.moveSpeed = defaultMoveSpeed * speedMultiplier;

            speedPotionEndTime = Time.time + addDurationSeconds;
            speedPotionCoroutine = StartCoroutine(SpeedPotionRoutine());
            return;
        }

        speedPotionEndTime += addDurationSeconds;
    }

    private IEnumerator SpeedPotionRoutine()
    {
        while (Time.time < speedPotionEndTime)
            yield return null;

        if (player != null)
            player.moveSpeed = defaultMoveSpeed;

        speedPotionActive = false;
        speedPotionCoroutine = null;
    }

    public void ApplyOrExtendStrengthPotion(float addDurationSeconds, float boostedCritChance)
    {
        if (stats == null)
            return;

        if (!strengthPotionActive)
        {
            strengthPotionActive = true;

            defaultCritChance = stats.critChance;
            stats.critChance = boostedCritChance;

            strengthPotionEndTime = Time.time + addDurationSeconds;
            strengthPotionCoroutine = StartCoroutine(StrengthPotionRoutine());
            return;
        }

        strengthPotionEndTime += addDurationSeconds;
    }

    private IEnumerator StrengthPotionRoutine()
    {
        while (Time.time < strengthPotionEndTime)
            yield return null;

        if (stats != null)
            stats.critChance = defaultCritChance;

        strengthPotionActive = false;
        strengthPotionCoroutine = null;
    }

    public void ClearAllBuffs()
    {
        if (speedPotionCoroutine != null)
            StopCoroutine(speedPotionCoroutine);

        if (strengthPotionCoroutine != null)
            StopCoroutine(strengthPotionCoroutine);

        if (player != null && speedPotionActive)
            player.moveSpeed = defaultMoveSpeed;

        if (stats != null && strengthPotionActive)
            stats.critChance = defaultCritChance;

        speedPotionActive = false;
        speedPotionCoroutine = null;
        speedPotionEndTime = 0f;

        strengthPotionActive = false;
        strengthPotionCoroutine = null;
        strengthPotionEndTime = 0f;
    }
}
