using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [Header("HealthBar UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private TextMeshProUGUI healthText;
    
    [Header("Damage Notification UI")]
    [SerializeField] private Image DamageNotification;
    private Color targetColor = new Color(101f / 255f, 0, 0, 49f / 255f);
    private float fadeDuration = 0.2f;
    private float timer = 0f;

    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private float waveDisplayDuration = 1f;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;

    [Header("Skill UI")]
    [SerializeField] private Image baitImage;
    private float baitCooldown;
    [SerializeField]private Image flashImage;
    private float flashCooldown;
    [SerializeField]private Image turretImage;
    private float turretCooldown;
    [SerializeField]private Image landmineImage;
    private float landmineCooldown;

    [Header("Buff UI")]
    [SerializeField] private PlayerBuffController buffController;
    [SerializeField] private Image speedPotionBuffImage;
    [SerializeField] private Image strengthPotionBuffImage;

    private float lastHealth;
    private bool isGameOverShown;

    private void Start()
    {
        UpdateHealthUI();
        UpdateBuffUI();

        baitCooldown=SkillManager.instance.bait.cooldown;
        flashCooldown=SkillManager.instance.flash.cooldown;
        turretCooldown=SkillManager.instance.turret.cooldown;
        landmineCooldown=SkillManager.instance.landmine.cooldown;
    }

    private void Update()
    {
        SkillUICooldownCheck();
        UpdateBuffUI();
    }

    private void UpdateBuffUI()
    {
        speedPotionBuffImage.enabled = buffController.speedPotionActive;
        strengthPotionBuffImage.enabled = buffController.strengthPotionActive;
    }

    private void SkillUICooldownCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SetCooldownOf(baitImage);
        if (Input.GetKeyDown(KeyCode.LeftShift))
            SetCooldownOf(flashImage);
        if (Input.GetKeyDown(KeyCode.F))
            SetCooldownOf(turretImage);
        if (Input.GetKeyDown(KeyCode.Q))
            SetCooldownOf(landmineImage);

        CheckCooldown(baitImage, baitCooldown);
        CheckCooldown(flashImage, flashCooldown);
        CheckCooldown(turretImage, turretCooldown);
        CheckCooldown(landmineImage, landmineCooldown);
    }

    private void OnEnable()
    {
        if (stats != null)
            stats.OnHealthChanged += UpdateHealthUI;

        if (ScoreManager.instance != null)
            ScoreManager.instance.OnScoreChanged += UpdateScoreUI;

        UpdateScoreUI(0);
    }

    private void OnDisable()
    {
        if (stats != null)
            stats.OnHealthChanged -= UpdateHealthUI;

        if (ScoreManager.instance != null)
            ScoreManager.instance.OnScoreChanged -= UpdateScoreUI;
    }

    private void UpdateHealthUI()
    {
        healthSlider.maxValue = stats.maxHealth;
        healthSlider.value = stats.currentHealth;
        healthText.text = $"{stats.currentHealth} / {stats.maxHealth}";

        if (lastHealth > stats.currentHealth)
            StartCoroutine(ShowDamageNotification());

        lastHealth = stats.currentHealth;

        if (!isGameOverShown && stats.currentHealth <= 0)
            ShowGameOver();
    }

    private void SetCooldownOf(Image image)
    {
        if(image.fillAmount <= 0)
            image.fillAmount = 1;
    }

    private void CheckCooldown(Image image,float cooldown)
    {
        if (image.fillAmount > 0)
            image.fillAmount -= Time.deltaTime / cooldown;
    }

    private IEnumerator ShowDamageNotification()
    {
        timer= 0;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Sin(timer / fadeDuration * Mathf.PI);
            DamageNotification.color = Color.Lerp(Color.clear, targetColor, alpha);
            yield return null;
        }

        DamageNotification.color = Color.clear;
    }

    private void UpdateScoreUI(int score)
    {
        scoreText.text = $"SCORE: {score}";
    }

    private void ShowGameOver()
    {
        isGameOverShown = true;

        gameOverUI.SetActive(true);

        Time.timeScale = 0;
    }

    public void ShowWave(int wave)
    {
        StopCoroutine(nameof(ShowWaveRoutine));
        StartCoroutine(ShowWaveRoutine(wave));
    }

    private IEnumerator ShowWaveRoutine(int wave)
    {
        waveText.gameObject.SetActive(true);
        waveText.text = $"Wave {wave}";
        yield return new WaitForSeconds(waveDisplayDuration);
        waveText.gameObject.SetActive(false);
    }
}
