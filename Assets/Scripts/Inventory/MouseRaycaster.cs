using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
    public LayerMask itemLayer;
    public float interactionDistance = 3f;
    private Items currentHoveredItem;

    [Header("UI References")]
    [SerializeField] private GameObject Inventory;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hitItem = Physics.Raycast(ray, out hit, interactionDistance, itemLayer);

        if (hitItem)
        {
            Items itemWorld = hit.collider.GetComponent<Items>();
            if (itemWorld != null && itemWorld != currentHoveredItem)
            {
                currentHoveredItem = itemWorld;
            }
        }
        else if (currentHoveredItem != null)
        {
            currentHoveredItem = null;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentHoveredItem != null)
            {
                if (InventoryManager.Instance.inventory.Contains(currentHoveredItem.data))
                {
                    Vector3 spawnPos = transform.position + transform.forward * 1.5f;
                    InventoryManager.Instance.DropItem(currentHoveredItem.data, spawnPos);
                    InventoryManager.Instance.RemoveItemFromInventory(currentHoveredItem.data);
                    currentHoveredItem = null;
                }
            }
            else if (InventoryManager.Instance.currentlyEquippedItem != null)
            {
                Vector3 spawnPos = transform.position + transform.forward * 1.5f;
                InventoryManager.Instance.DropEquippedItem(spawnPos);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && currentHoveredItem != null)
        {
            if (currentHoveredItem.data.isEquippable)
            {
                InventoryManager.Instance.EquipItem(currentHoveredItem.data);
                currentHoveredItem = null;
            }
        }
    }
}

