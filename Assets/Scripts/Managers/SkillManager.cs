using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Bait_Skill bait { get; private set; }
    public Flash_Skill flash { get; private set; }
    public Turret_Skill turret { get; private set; }
    public Landmine_Skill landmine { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);
    }

    private void Start()
    {
        bait = GetComponent<Bait_Skill>();
        flash = GetComponent<Flash_Skill>();
        turret = GetComponent<Turret_Skill>();
        landmine = GetComponent<Landmine_Skill>();
    }
}
