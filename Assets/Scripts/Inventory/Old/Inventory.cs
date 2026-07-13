using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject InventoryGroup;
    [SerializeField] private GameObject Crosshair;


    void Start()
    {
        if (InventoryGroup != null)
        {
            InventoryGroup.SetActive(false);
            Crosshair.SetActive(true);
        }
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleInventoryUI();
        }
        */
    }

    private void ToggleInventoryUI()
    {
        if (InventoryGroup != null)
        {
            bool isOpen = !InventoryGroup.activeSelf;
            InventoryGroup.SetActive(isOpen);
            Crosshair.SetActive(!isOpen);

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