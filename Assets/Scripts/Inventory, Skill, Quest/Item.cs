using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Item : MonoBehaviour
{
    public string itemName;
    public int itemQuantity;
    public Sprite itemSprite;
    [TextArea]public string itemDescription;
    public ItemType itemType;
    
    private InventoryManager inventoryManager;

    private ItemSOLibrary itemSOLibrary;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        itemSOLibrary = GameObject.Find("InventoryManager").GetComponent<ItemSOLibrary>();
    }

    //if player collides with an item, call function AddItem from InventoryManager with 3 parameter 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Call event
            GameEventManager.instance.collectedQuestItemEvent.CollectedQuestItem(this.gameObject);
            //GameEventManager.instance.miscEvent.ItemCollected();

            //Get the number of left over item after add in to slot item 
            int leftOverItems = inventoryManager.AddItem(itemName, itemQuantity, itemSprite, itemDescription, itemType);
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
