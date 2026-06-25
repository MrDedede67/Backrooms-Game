using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField]
    private GameObject InventoryGroup; // The UI element containing your inventory layout

    void Start()
    {
        if (InventoryGroup != null)
        {
            InventoryGroup.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventoryUI();
        }
    }

    private void ToggleInventoryUI()
    {
        if (InventoryGroup != null)
        {
            bool isOpen = !InventoryGroup.activeSelf;
            InventoryGroup.SetActive(isOpen);

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