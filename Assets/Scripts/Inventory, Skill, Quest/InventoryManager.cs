using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryMenu;
    private bool menuActivated;

    [SerializeField] private List<ItemSlot> ItemSlots;
    [SerializeField] private ItemSO[] itemSOs;

    private void Awake()
    {
        InventoryMenu.SetActive(false);
    }

    void Update()
    {
        //put key E down to open/close Inventory Menu
        if (Input.GetButtonDown("InventoryMenu") && menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
        
        else if (Input.GetButtonDown("InventoryMenu") && !menuActivated)
        {
            Time.timeScale = 0;
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public bool UseItem(string _itemName)
    {
        for (int i = 0; i < itemSOs.Length; i++)
        {
            //If item of Scriptable Object have itemName is the same as _itemName
            if(itemSOs[i].itemName == _itemName)
            {
                //Return bool canUse if item can be used
                bool canUse = itemSOs[i].UseItem();
                return canUse;
            }
        }
        return false;
    }

    public int AddItem(string _itemName, int _itemQuantity, Sprite _itemSprite, string _itemdescription)
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            if (ItemSlots[i].isFull == false && ItemSlots[i].itemName == _itemName || ItemSlots[i].itemQuantity == 0)
            {
                //if itemslot isn't full and have same item name or quantity = 0, call AddItem from ItemSlot
                int leftOverItems = ItemSlots[i].AddItem(_itemName, _itemQuantity, _itemSprite, _itemdescription);
                if(leftOverItems > 0)
                {
                    //if number of left over item > 0, call recursive function AddItem with quantity is leftOverItems
                    leftOverItems = AddItem(_itemName, leftOverItems, _itemSprite, _itemdescription);
                }
                //return number of left over item to prevent add into all item slot
                return leftOverItems;
            }
        }
        return _itemQuantity;
    }

    //this function is called by ItemSlot function 
    public void DeSelectAllSlot()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            //Deselect all item slot and set this slot not have item
            ItemSlots[i].selectedShader.SetActive(false);
            ItemSlots[i].thisItemSelected = false;
        }
    }
}
