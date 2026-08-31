using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum ItemType
    {
        speedPotion,
        healthPotion,
        strengthPotion
    }

    [SerializeField]private ItemType itemType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerStats = other.GetComponent<PlayerStats>();
            if (playerStats == null)
                return;

            var player = other.GetComponent<Player>();
            if (player == null)
                return;
            
            switch (itemType)
            {
                case ItemType.healthPotion:
                    playerStats.Heal(10);
                    break;
                case ItemType.strengthPotion:
                    (player.buffs ?? player.GetComponent<PlayerBuffController>())?.ApplyOrExtendStrengthPotion(10f, 0.7f);
                    break;
                case ItemType.speedPotion:
                    (player.buffs ?? player.GetComponent<PlayerBuffController>())?.ApplyOrExtendSpeedPotion(10f, 1.2f);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
