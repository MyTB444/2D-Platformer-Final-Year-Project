using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create data type called InventoryItemSO and create a menu navigation on the editor

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class InventoryItemSO : ScriptableObject
{
    public Sprite icon;
    public bool isKey;
    public bool grantsDoubleJump; 

}