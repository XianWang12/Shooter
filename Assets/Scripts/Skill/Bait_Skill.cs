using UnityEngine;

public class Bait_Skill : Skill
{
    [Header("Bait info")]
    [SerializeField]private GameObject baitPrefab;
    
    [SerializeField]private float baitDamage;
    [SerializeField]private float baitBlastRadius;
    [SerializeField]private float baitSpeed;
    [SerializeField]private float baitDuration;
    public override void Use()
    {
        GameObject currentBait = Instantiate(baitPrefab, player.transform.position, player.transform.rotation);

        Bait_Skill_Controller baitController = currentBait.GetComponent<Bait_Skill_Controller>();
        baitController.SetUpClone(baitDuration, baitDamage, baitBlastRadius, baitSpeed);

        SetInvincibility();

        Invoke("ResetInvincibility", baitDuration);
    }

    private void SetInvincibility() => player.stats.isInvincible = true;

    private void ResetInvincibility() => player.stats.isInvincible = false;
}
