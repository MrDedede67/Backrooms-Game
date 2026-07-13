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
}