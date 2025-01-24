using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{   
    public InventorySlot slot;
    public InventoryManager inventoryManager;
    public bool droppable;
    public Image image;
    public Color selectedColor, notSelectedColor;

    public void Awake() {
        Deselect();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }
    public void Select() {
        image.color = selectedColor;
    }

    public void Deselect() {
       image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0) {
            if(droppable){
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
            }
        };
    }
    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.clickCount == 2) {
           bool result = inventoryManager.StackItem(slot);
        }
    }

}
