using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //------ITEM DATA------//
    public string itemName;
    public int itemQuantity;
    private Sprite itemSprite;
    private string itemDescription;
    public bool isFull;
    public ItemType itemType;

    //------ITEM SLOT------//
    [SerializeField] private TMP_Text itemQuantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Sprite emptySprite;
    public GameObject selectedShader;
    public bool thisItemSelected;
    [SerializeField] private int maxNumberOfItem;

    //------ITEM DESCRIPTION------//
    [SerializeField] private Tooltip toolTip;


    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    private void Start()
    {
        
    }

    //Add info of item to this slot
    public int AddItem(string _itemName, int _itemQuantity, Sprite _itemSprite, string _itemDescription, ItemType _itemType)
    {
        if (isFull)
        {
            //Check to see if the slot already full, return number of left over item
            return _itemQuantity;
        }

        //Set the itemType
        this.itemType = _itemType;

        //Set the itemName
        this.itemName = _itemName;
        //Set the item sprite
        this.itemSprite = _itemSprite;
        itemImage.sprite = itemSprite;
        //Set the item description
        this.itemDescription = _itemDescription;

        //Set the item quantity
        this.itemQuantity += _itemQuantity;

        //if this quantity of item is greater than or equal maximum number of items for this slot
        if(this.itemQuantity >= maxNumberOfItem)
        {
            itemQuantityText.text = maxNumberOfItem.ToString();
            itemQuantityText.enabled = true;

            //this slot already have full item 
            isFull = true;

            //Return left over item
            int leftOverItems = _itemQuantity - maxNumberOfItem;
            itemQuantity = maxNumberOfItem;
            return leftOverItems;
        }

        //Set text quantity, if this item slot can contain all item, set quantity and return number of left over item is 0
        itemQuantityText.enabled = true;

        itemQuantityText.text = this.itemQuantity.ToString();
        return 0;
    }

    //if click on this slot 
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        //Using item by selecting item twice
        if (thisItemSelected)
        {
            //Check if item can be used from InventoryManager, return bool canUse
            bool canUse = inventoryManager.UseItem(itemName);
            if(canUse)
            {
                isFull = false;
                itemQuantity -= 1;
                itemQuantityText.text = itemQuantity.ToString();
                if (itemQuantity <= 0)
                {
                    EmptySlot();
                }
            }
        }

        else
        {
            //deselect all slot and set this slot currently sellected
            inventoryManager.DeSelectAllSlot();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
        
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

        //Create and modify Rigidbody@d
        Rigidbody2D rigidbody2D = itemToDrop.AddComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;

        //Set the location
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(1f, 0, 0);
        itemToDrop.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

        //Minus quantity item drop
        itemQuantity -= 1;
        isFull = false;
        itemQuantityText.text = itemQuantity.ToString();
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
        itemQuantityText.enabled = false;
        itemName = "";
        itemDescription = "";
        itemImage.sprite = emptySprite;
    }
}
