using UnityEngine;

public class Flash_Skill : Skill
{
    [Header("Flash")]
    [SerializeField] private float flashDistance;

    public override void Use()
    {
        player.rb.MovePosition(TargetPos());
    }

    private Vector3 TargetDir()
    {
        if (player.moveDir != Vector3.zero)
            return player.moveDir.normalized;
        return player.transform.forward;
    }

    private Vector3 TargetPos()
    {
        RaycastHit hit;
        Physics.Raycast(player.transform.position, TargetDir(), out hit, flashDistance, player.groundLayer);
        if (hit.collider == null)
            return player.transform.position + TargetDir() * flashDistance;
        return hit.point;
    }

}
