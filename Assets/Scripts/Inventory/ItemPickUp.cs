using UnityEngine;
using UnityEngine.UI; 

public class ItemPickUp : MonoBehaviour
{
    [SerializeField] private float reach = 5f;
    [SerializeField] private LayerMask item;

    [Header("Crosshair Settings")]
    [SerializeField] private Image crosshair;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color highlightColor = Color.red;

    void Update()
    {
        crosshair.color = defaultColor;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, reach, item))
        {
            crosshair.color = highlightColor;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Items itemComponent = hit.transform.GetComponent<Items>();

                if (itemComponent != null)
                {
                    InventoryManager.Instance.AddToInventory(itemComponent.data);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}