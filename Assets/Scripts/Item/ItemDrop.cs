using UnityEngine;

[System.Serializable]
public class Item
{   
    public GameObject prefab;
    [Range(0f, 100f)] public float dropChance;
}

public class ItemDrop : MonoBehaviour
{
    public Item[] items;

    public void DropItem()
    {
        foreach (var item in items)
        {
            if (Random.Range(0f, 100f) <= item.dropChance)
            {
                Instantiate(item.prefab, transform.position, Quaternion.identity);
            }
        }
    }
}
