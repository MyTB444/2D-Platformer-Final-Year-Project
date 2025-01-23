using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class InventorySystem : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject itemSlotPrefab;
    public int maxItems = 3;
    private InventoryItemSO[] inventory;

    private void Start()
    {
        inventory = new InventoryItemSO[maxItems];
        UpdateUI();
    }

    public bool AddItem(InventoryItemSO newItem)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = newItem;
                UpdateUI();
                return true;
            }
        }
        Debug.Log("Inventory is full!");
        return false;
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < inventory.Length && inventory[index] != null)
        {
            inventory[index] = null;
            UpdateUI();
        }
        else
        {
            Debug.Log("Invalid slot or no item to remove.");
        }
    }

    private void UpdateUI()
    {
        // Clear existing slots
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Populate slots
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                GameObject slot = Instantiate(itemSlotPrefab, inventoryPanel.transform);
                Image itemImage = slot.GetComponent<Image>();

                itemImage.sprite = inventory[i].icon;
            }
        }
    }

    public InventoryItemSO GetItem(int index)
    {
        if (index >= 0 && index < inventory.Length)
        {
            return inventory[index];
        }
        return null;
    }
}
