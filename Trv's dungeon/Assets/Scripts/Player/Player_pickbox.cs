using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_pickbox : MonoBehaviour
{
    private Player player;
    public InventoryItemSO key;
    private Game_man _gameman;
    public InventoryItemSO boots;
    void Start()
    {
        player = GetComponentInParent<Player>();
        if (GameObject.FindGameObjectWithTag("Gameman") != null)
        {
            _gameman = GameObject.FindGameObjectWithTag("Gameman").GetComponent<Game_man>();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Diamond")
        {
            Destroy(other.gameObject);
            _gameman.DiamonFound();
        }
        else if (other.gameObject.name == "Hearth")
        {
            Destroy(other.gameObject);
            _gameman.HearthFound();
        }
        else if (other.gameObject.tag == "Key")
        {
            InventorySystem inventory = FindObjectOfType<InventorySystem>();
            inventory.AddItem(key);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name == "Boots")
        {
            InventorySystem inventory = FindObjectOfType<InventorySystem>();
            inventory.AddItem(boots);
            player.EnableDoubleJump();
            Destroy(other.gameObject);
        }
    }
}
