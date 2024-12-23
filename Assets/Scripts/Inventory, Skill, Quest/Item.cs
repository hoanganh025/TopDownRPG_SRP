using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] public string itemName;
    [SerializeField] public int itemQuantity;
    [SerializeField] public Sprite itemSprite;
    [TextArea][SerializeField] public string itemDescription;
    
    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    //if player collides with an item, call function AddItem from InventoryManager with 3 parameter 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Get the number of left over item after add in to slot item 
            int leftOverItems = inventoryManager.AddItem(itemName, itemQuantity, itemSprite, itemDescription);
            //If can add all item into item slot, destroy gameObjects
            if(leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            //Else change item quantity and keep the item 
            else
            {
                itemQuantity = leftOverItems;
                Debug.Log("Kho do da day");
            }
        }
    }

}
