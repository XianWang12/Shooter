using UnityEngine;

public class Turret_Skill : Skill
{
    [Header("Turret info")]
    [SerializeField] private GameObject turretPrefab;

    [SerializeField] private float turretDamage;
    [SerializeField] private float turretDetectRadius;
    [SerializeField] private float turretDuration;
    [SerializeField] private float turretAttackInterval;

    public override void Use()
    {
        GameObject currentTurret = Instantiate(turretPrefab, player.transform.position, player.transform.rotation);
        Turret_Skill_Controller turretController = currentTurret.GetComponentInChildren<Turret_Skill_Controller>();
        turretController.SetUpTurret(turretDuration, turretDamage, turretDetectRadius, turretAttackInterval);
    }
}
