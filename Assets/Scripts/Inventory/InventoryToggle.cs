using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject Inventory;

    [SerializeField] private PlayerMovement playerMovement;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleInventoryUI();
        }
    }

    private void ToggleInventoryUI()
    {
        if (Inventory != null)
        {
            bool isOpen = !Inventory.activeSelf;
            Inventory.SetActive(isOpen);

            if (playerMovement != null)
            {
                playerMovement.isPaused = isOpen;
            }

            if (isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
