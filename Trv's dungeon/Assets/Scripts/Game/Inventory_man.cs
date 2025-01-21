using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public Sprite icon;
        public string name;
    }

    public GameObject inventoryPanel;
    public GameObject itemSlotPrefab;

    private List<InventoryItem> inventory = new List<InventoryItem>();

    public int maxItems = 3; // Maximum number of items allowed

    public void AddItem(InventoryItem newItem)
    {
        if (inventory.Count < maxItems && !inventory.Exists(item => item.name == newItem.name))
        {
            inventory.Add(newItem); // Add item if it's not already in the inventory
            UpdateUI();
        }
        else
        {
            Debug.Log("Inventory is full or item already exists.");
        }
    }

    private void UpdateUI()
    {
        for (int i = inventoryPanel.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = inventoryPanel.transform.GetChild(i);
            if (child != null) // Ensure the object is valid
            {
                Destroy(child.gameObject); // Destroy the object
            }
        }

        // Add each item in the inventory to the UI
       
    }
}

