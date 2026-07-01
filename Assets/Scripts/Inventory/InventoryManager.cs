using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> inventory = new List<ItemData>();

    void Awake() => Instance = this;

    public void AddToInventory(ItemData item)
    {
        inventory.Add(item);
        Debug.Log("Added to inventory: " + item.itemName);
    }
}