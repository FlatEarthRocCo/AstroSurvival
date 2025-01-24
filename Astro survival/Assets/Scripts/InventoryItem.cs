using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Item item; 

    [Header("UI")]
    public Image image;
    public Text countText;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    public InventoryManager inventoryManager;

    public void InitialiseItem(Item newItem) {
        item = newItem;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount() {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {   
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            inventoryManager = parentAfterDrag.GetComponent<InventorySlot>().inventoryManager;
            bool result = inventoryManager.ItemsInSlotDivision(transform.gameObject, item, parentAfterDrag.GetComponent<InventorySlot>().slot, 1);
        } 
        if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            inventoryManager = parentAfterDrag.GetComponent<InventorySlot>().inventoryManager;
            bool result = inventoryManager.ItemsInSlotDivision(transform.gameObject, item, parentAfterDrag.GetComponent<InventorySlot>().slot, -1);
        }
        transform.localPosition = Input.mousePosition;   
    }

public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }


}


