using UnityEngine;

public class MouseRaycaster : MonoBehaviour
{
    public LayerMask itemLayer;
    public float interactionDistance = 3f;
    private Items currentHoveredItem;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, itemLayer))
        {
            Items itemWorld = hit.collider.GetComponent<Items>();

            if (itemWorld != null && itemWorld != currentHoveredItem)
            {
                currentHoveredItem = itemWorld;
                Debug.Log("Now hovering over: " + currentHoveredItem.data.itemName);
            }

            if (itemWorld != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (InventoryManager.Instance.inventory.Contains(itemWorld.data))
                    {
                        Vector3 spawnPos = transform.position + transform.forward * 1.5f;
                        InventoryManager.Instance.DropItem(itemWorld.data, spawnPos);

                        currentHoveredItem = null;
                    }
                }
            }
        }
        else
        {
            if (currentHoveredItem != null)
            {
                Debug.Log("Stopped hovering over: " + currentHoveredItem.data.itemName);
                currentHoveredItem = null;
            }
        }
    }
}
