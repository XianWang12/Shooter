using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine_Skill : Skill
{
    [Header("Landmine info")]
    [SerializeField] private GameObject landminePrefab;

    [SerializeField] private float landmineDamage;
    [SerializeField] private float landmineBlastRadius;

    public override void Use()
    {
        GameObject currentLandmine = Instantiate(landminePrefab, player.transform.position, player.transform.rotation);
        Landmine_Skill_Controller landmineController = currentLandmine.GetComponent<Landmine_Skill_Controller>();
        landmineController.SetUpClone(landmineDamage, landmineBlastRadius);
    }
}
