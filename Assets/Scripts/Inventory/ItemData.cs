using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Base Information")]
    public int id;
    public string itemName;
    public Sprite icon;
    public GameObject prefab;

    [Header("Functions")]
    public bool isEquippable;
    public bool isConsumeable;
}