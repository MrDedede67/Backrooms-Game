using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class ItemUIList
{
    public string itemName;
    public GameObject[] uiObjects;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> inventory = new List<ItemData>();

    [SerializeField] private List<ItemUIList> itemDisplaySettings;

    private Dictionary<string, GameObject[]> itemLookup = new Dictionary<string, GameObject[]>();

    void Awake()
    {
        Instance = this;
        foreach (var setting in itemDisplaySettings)
        {
            itemLookup[setting.itemName] = setting.uiObjects;
        }
    }

    public bool AddToInventory(ItemData item)
    {
        int currentCount = inventory.Count(i => i.itemName == item.itemName);

        if (currentCount < 3)
        {
            inventory.Add(item);
            UpdateVisuals(item.itemName, currentCount);
            return true;
        }
        return false;
    }

    private void UpdateVisuals(string itemName, int index)
    {
        if (itemLookup.ContainsKey(itemName) && index < itemLookup[itemName].Length)
        {
            itemLookup[itemName][index].SetActive(true);
        }
    }

    public void DropItem(ItemData item, Vector3 spawnPosition)
    {
        ItemData itemToRemove = inventory.FirstOrDefault(i => i.itemName == item.itemName);

        if (itemToRemove != null)
        {
            inventory.Remove(itemToRemove);

            RefreshUI(item.itemName);

            Instantiate(item.prefab, spawnPosition, Quaternion.identity);
        }
    }

    private void RefreshUI(string itemName)
    {
        if (itemLookup.ContainsKey(itemName))
        {
            GameObject[] uiSlots = itemLookup[itemName];
            int currentCount = inventory.Count(i => i.itemName == itemName);

            for (int i = 0; i < uiSlots.Length; i++)
            {

                uiSlots[i].SetActive(i < currentCount);
            }
        }
    }
}