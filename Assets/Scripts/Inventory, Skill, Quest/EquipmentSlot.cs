using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //------EQUIPMENT DATA------//
    public string itemName;
    public int itemQuantity;
    private Sprite itemSprite;
    private string itemDescription;
    public bool isFull;
    public ItemType itemType;

    //------EQUIPMENT SLOT------//
    [SerializeField] private EquippedSlot headSlot, cloackSlot, bodySlot, legsSlot, mainHandSlot, offHandSlot, relicSlot, feetSlot;
    [SerializeField] private Image itemImage;
    [SerializeField] private Sprite emptySprite;
    public GameObject selectedShader;
    public bool thisItemSelected;

    //------ITEM DESCRIPTION------//
    [SerializeField] private Tooltip toolTip;


    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    //Add info of item to this slot
    public int AddItem(string _itemName, int _itemQuantity, Sprite _itemSprite, string _itemDescription, ItemType _itemType)
    {
        if (isFull)
        {
            //Check to see if the slot already full, return number of left over item
            return _itemQuantity;
        }

        //Set the equipmentType
        this.itemType = _itemType;

        //Set the equipmentName
        this.itemName = _itemName;
        //Set the equipment sprite
        this.itemSprite = _itemSprite;
        itemImage.sprite = itemSprite;
        //Set the equipment description
        this.itemDescription = _itemDescription;

        //Set the equipment quantity
        this.itemQuantity = 1;

        isFull = true;
        return 0;
    }

    //if click on this slot 
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        //Using item by selecting item twice
        if (thisItemSelected)
        {
            EquipGear();
        }

        else
        {
            //deselect all slot and set this slot currently sellected
            inventoryManager.DeSelectAllSlot();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    //If equip gear, move equipment to EquippedSlot by type
    private void EquipGear()
    {
        if (itemType == ItemType.head)
            headSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.cloak)
            cloackSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.body)
            bodySlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.legs)
            legsSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.mainHand)
            mainHandSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.offHand)
            offHandSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.relic)
            relicSlot.EquipGear(itemSprite, itemName, itemDescription);
        if (itemType == ItemType.feet)
            feetSlot.EquipGear(itemSprite, itemName, itemDescription);

        //After that, reset data of this equipment slot
        EmptySlot();
    }

    private void OnRightClick()
    {
        //Create new item
        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.itemName = itemName;
        newItem.itemQuantity = 1;
        newItem.itemDescription = itemDescription;
        newItem.itemSprite = itemSprite;
        newItem.itemType = itemType;

        //Crate and modify SpriteRenderer
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sortingLayerName = ("Foreground");
        sr.sprite = itemSprite;
        sr.sortingOrder = -1;

        //Create and modify Collider2D
        Collider2D collider = itemToDrop.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;

        //Create and modify Rigidbody2d
        Rigidbody2D rigidbody2D = itemToDrop.AddComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;

        //Set the location
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1f, 0, 0);
        itemToDrop.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

        //Minus quantity item drop
        itemQuantity -= 1;
        isFull = false;
        if (itemQuantity <= 0)
        {
            EmptySlot();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If item slot not have item name or item name is white space, don't show tooltip
        if (!string.IsNullOrWhiteSpace(itemName))
        {
            toolTip.SetContent(itemName, itemDescription);
            toolTip.ShowToolTip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTip.HideToolTip();
    }

    private void EmptySlot()
    {
        isFull = false;
        itemName = "";
        itemQuantity = 0;
        itemDescription = "";
        itemType = ItemType.none;
        itemImage.sprite = emptySprite;
    }
}
