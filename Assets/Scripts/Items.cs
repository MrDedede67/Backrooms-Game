using UnityEngine;

public class Items : MonoBehaviour
{
    public int Id { get; set; }
    public string itemName { get; set; }
    public int Amount { get; set; }

    public Items(int id, string name, int amount)
    {
        Id = id;
        itemName = name;
        Amount = amount;
    }
}
