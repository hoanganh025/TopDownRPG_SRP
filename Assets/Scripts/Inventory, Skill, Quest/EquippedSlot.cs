using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    //Slot Appearence
    [SerializeField] private Image slotImage; //image attach in inspector 
    [SerializeField] private TMP_Text slotName;

    //Slot data
    [SerializeField] private ItemType itemType = new ItemType();

    [SerializeField] private Sprite itemSprite; //data image of this slot 
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;

    //other variable
    public bool slotInUse;
    public GameObject selectedShader;
    public bool thisItemSelected;
    [SerializeField] private Sprite emptySprite;
    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;

    private void Awake()
    {
        selectedShader.SetActive(false);
    }

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryManager").GetComponent<EquipmentSOLibrary>();
    }

    public void EquipGear(Sprite _itemSprite, string _itemName, string _itemDescription)
    {
        //If slot is already item equipped, send it back equipment before re-write data
        if(slotInUse)
        {
            UnEquipGear();
        }

        //Update Image
        this.itemSprite = _itemSprite;
        slotImage.sprite = itemSprite;
        slotName.enabled = false;

        //Update data
        this.itemName = _itemName;
        this.itemDescription = _itemDescription;
        slotInUse = true;

        //Deselect slot
        inventoryManager.DeSelectAllSlot();

        //Update player stat
        for (int i = 0; i < equipmentSOLibrary.equipmentSOs.Length; i++)
        {
            if(equipmentSOLibrary.equipmentSOs[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSOs[i].EquipGear();
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //On left click
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        //On right click
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        if(thisItemSelected && slotInUse)
        {
            UnEquipGear();
        }
        else
        {
            inventoryManager.DeSelectAllSlot();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    private void OnRightClick()
    {

    }

    private void UnEquipGear()
    {
        //Move item from EquippedSlot to inventory 
        inventoryManager.DeSelectAllSlot();
        inventoryManager.AddItem(itemName, 1, itemSprite, itemDescription, itemType);

        itemSprite = emptySprite;
        slotImage.sprite = itemSprite;
        slotName.enabled = true;
        slotInUse = false;

        //Update player stat
        for (int i = 0; i < equipmentSOLibrary.equipmentSOs.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSOs[i].itemName == itemName)
            {
                equipmentSOLibrary.equipmentSOs[i].UnEquipGear();
            }
        }
    }
}
