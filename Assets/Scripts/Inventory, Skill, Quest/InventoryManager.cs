using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryMenu;
    private bool menuActivated;

    [SerializeField] private List<ItemSlot> ItemSlots;
    [SerializeField] private List<EquipmentSlot> EquipmentSlots;
    [SerializeField] private List<EquippedSlot> EquippedSlots;

    private ItemSOLibrary itemSOLibrary;

    public delegate void dgtCloseInventory();
    public static event dgtCloseInventory _dgtCloseInventory;

    public static InventoryManager instance;
    private void Awake()
    {
        InventoryMenu.SetActive(false);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        itemSOLibrary = GetComponent<ItemSOLibrary>();
    }

    void Update()
    {
        //put key E down to open/close Inventory Menu
        //if (Input.GetButtonDown("InventoryMenu"))
        if (PlayerController.instance.inputController.Gameplay.Inventory.WasPressedThisFrame())
        {
            Inventory();
        }
    }

    private void Inventory()
    {
        //Close
        if ( menuActivated)
        {
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
            //Event inventory close
            _dgtCloseInventory?.Invoke();
        }

        //Open
        else
        {
            Time.timeScale = 0;
            
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public bool UseItem(string _itemName)
    {
        for (int i = 0; i < itemSOLibrary.itemSOs.Length; i++)
        {
            //If item of Scriptable Object have itemName is the same as _itemName
            if (itemSOLibrary.itemSOs[i].itemName == _itemName)
            {
                //Return bool canUse if item can be used
                bool canUse = itemSOLibrary.itemSOs[i].UseItem();
                return canUse;
            }
        }
        return false;
    }

    public int AddItem(string _itemName, int _itemQuantity, Sprite _itemSprite, string _itemdescription, ItemType _itemType)
    {
        if(_itemType == ItemType.comsumable || _itemType == ItemType.crafting || _itemType == ItemType.collectible || _itemType == ItemType.quest)
        {
            for (int i = 0; i < ItemSlots.Count; i++)
            {
                if (ItemSlots[i].isFull == false && ItemSlots[i].itemName == _itemName || ItemSlots[i].itemQuantity == 0)
                {
                    //if itemslot isn't full and have same item name or quantity = 0, call AddItem from ItemSlot
                    int leftOverItems = ItemSlots[i].AddItem(_itemName, _itemQuantity, _itemSprite, _itemdescription, _itemType);
                    if (leftOverItems > 0)
                    {
                        //if number of left over item > 0, call recursive function AddItem with quantity is leftOverItems
                        leftOverItems = AddItem(_itemName, leftOverItems, _itemSprite, _itemdescription, _itemType);
                    }
                    //return number of left over item to prevent add into all item slot
                    return leftOverItems;
                }
            }
            return _itemQuantity;
        }

        else
        {
            for (int i = 0; i < EquipmentSlots.Count; i++)
            {
                if (EquipmentSlots[i].isFull == false && EquipmentSlots[i].itemName == _itemName || EquipmentSlots[i].itemQuantity == 0)
                {
                    //if equipment slot isn't full and have same item name or quantity = 0, call AddItem from ItemSlot
                    int leftOverItems = EquipmentSlots[i].AddItem(_itemName, _itemQuantity, _itemSprite, _itemdescription, _itemType);
                    if (leftOverItems > 0)
                    {
                        //if number of left over item > 0, call recursive function AddItem with quantity is leftOverItems
                        leftOverItems = AddItem(_itemName, leftOverItems, _itemSprite, _itemdescription, _itemType);
                    }
                    //return number of left over item to prevent add into all equipment slot
                    return leftOverItems;
                }
            }
            return _itemQuantity;
        }
        
    }

    //this function is called by ItemSlot function 
    public void DeSelectAllSlot()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            //Deselect all item slot item and set this slot not have item
            ItemSlots[i].selectedShader.SetActive(false);
            ItemSlots[i].thisItemSelected = false;
        }

        for (int i = 0; i < EquipmentSlots.Count; i++)
        {
            //Deselect all item slot eqipment and set this slot not have item
            EquipmentSlots[i].selectedShader.SetActive(false);
            EquipmentSlots[i].thisItemSelected = false;
        }

        for (int i = 0; i < EquippedSlots.Count; i++)
        {
            //Deselect all item slot equiped and set this slot not have item
            EquippedSlots[i].selectedShader.SetActive(false);
            EquippedSlots[i].thisItemSelected = false;
        }
    }

}

public enum ItemType
{
    none,
    comsumable,
    crafting,
    collectible,
    head,
    cloak,
    body,
    legs,
    mainHand,
    offHand,
    relic,
    feet,
    quest
};
