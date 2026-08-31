using System;
using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public float damage;

    [Header("Bleeding Info")]
    public float bleedDamagePerTime;
    public float times;
    private bool isBleeding;
    private Coroutine bleedCoroutine;

    public event Action OnHealthChanged;

    public bool isDead { get; private set; }

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke();
    }

    public virtual void ResetStats()
    {
        isDead = false;
        currentHealth = maxHealth;
    }

    public virtual void DoDamage(CharacterStats target)
    {
        target.TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(0f, currentHealth - damage);

        OnHealthChanged?.Invoke();

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Die(); 
        }
    }

    public virtual void CauseBleedingTo(CharacterStats target)
    {
        if (isDead || target.isDead)
            return;
        if(target.isBleeding)
            target.StopCoroutine(target.bleedCoroutine);

        target.bleedCoroutine = target.StartCoroutine(target.BleedingCoroutine(target, bleedDamagePerTime,times));
        target.isBleeding = true;
    }

    public virtual IEnumerator BleedingCoroutine(CharacterStats target, float damagePerTime, float times)
    {
        float bleedTime = 0f;
        while (bleedTime < times)
        {
            bleedTime += 1f;
            target.TakeDamage(damagePerTime);
            yield return new WaitForSeconds(1f);
        }
        target.isBleeding = false;
    }

    public virtual void Heal(float amount)
    {
        if (isDead)
            return;

        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke();
    }

    protected virtual void Die()
    {
        isDead = true;
    }
}
