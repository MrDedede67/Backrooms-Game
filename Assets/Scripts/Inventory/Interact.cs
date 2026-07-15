using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    [SerializeField] private float reach = 5f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("Crosshair Settings")]
    [SerializeField] private Image crosshair;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color highlightColor = Color.red;

    [Header("UI References")]
    [SerializeField] private GameObject Inventory;

    void Update()
    {
        crosshair.color = defaultColor;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, reach, interactableLayer))
        {
            crosshair.color = highlightColor;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Items itemComponent = hit.transform.GetComponent<Items>();
                if (itemComponent != null)
                {
                    bool wasAdded = InventoryManager.Instance.AddToInventory(itemComponent.data);
                    if (wasAdded)
                    {
                        Destroy(hit.transform.gameObject);
                    }
                }
                else
                {
                    IInteractable interactable = hit.transform.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        float speedToUse = Input.GetKey(KeyCode.LeftShift) ? 4f : 2f;

                        interactable.Interact(speedToUse);
                    }
                }
            }
        }
    }
}