using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class InventoryItemSO : ScriptableObject
{
    public Sprite icon;
    public bool isKey;
    public bool grantsDoubleJump; 

}