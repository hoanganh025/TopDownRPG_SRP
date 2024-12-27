using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] Canvas parentCanvas;
    [SerializeField] private TMP_Text itemDescriptionName;
    [SerializeField] private TMP_Text itemDescriptionText;
    public bool isShow = false;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryManager._dgtCloseInventory += HideToolTip;
    }

    private void OnDisable()
    {
        InventoryManager._dgtCloseInventory -= HideToolTip;
    }

    public void ShowToolTip()
    {
        Vector2 mousePos = Input.mousePosition;
        RectTransform canvasRect = parentCanvas.transform as RectTransform;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, mousePos, parentCanvas.worldCamera, out Vector2 localPoint);
        gameObject.transform.localPosition = localPoint;
        
        gameObject.SetActive(true);

        RectTransform tooltipRect = gameObject.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipRect);

        isShow = true;
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        isShow = false;
    }

    public void SetContent(string _itemName, string _itemDescription)
    {
        itemDescriptionName.text = _itemName;
        itemDescriptionText.text = _itemDescription;
    }
}
