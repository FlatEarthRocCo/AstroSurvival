using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public InventorySlot[] inventorySlots;
    public List<InventorySlot> openSlots;
    public GameObject inventoryItemPrefab;

    public int maxStackedItems = 10;

    int selectedSlot = -1;

    private void Start() {
        ChangeSelectedSlot(0);
    }

    private void Update() {
        if (Input.inputString != null) {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 6) {
                ChangeSelectedSlot(number - 1);
            }
        }
        GameObject[] openSlotsGO = GameObject.FindGameObjectsWithTag("Open");
        foreach(GameObject slotGO in openSlotsGO) {
            InventorySlot slot = slotGO.GetComponent<InventorySlot>();
            if (!openSlots.Contains(slot)) {
            openSlots.Add(slot);
            }
        }

    }

    void ChangeSelectedSlot(int newValue) {
        if (selectedSlot >= 0) {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item) {
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackedItems &&
                itemInSlot.item.stackable == true) {
                
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    public bool ChangeItemAmountInSlot(InventoryItem DraggedItem, Item item, InventorySlot slot, int direction) {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackedItems &&
                itemInSlot.item.stackable == true) {
                if(direction > 0) {
                    itemInSlot.count++;
                    DraggedItem.count--;
                    if (DraggedItem.count <= 0) {
                            Destroy(DraggedItem.gameObject);
                    } else {
                            DraggedItem.RefreshCount();
                    }
                    itemInSlot.RefreshCount();
                } else{
                    itemInSlot.count--;
                    DraggedItem.count++;
                    if (itemInSlot.count <= 0) {
                            Destroy(itemInSlot.gameObject);
                        } else {
                            itemInSlot.RefreshCount();
                    }
                    DraggedItem.RefreshCount();
                }
                
                itemInSlot.RefreshCount();
                return true;
            }
            
            if (itemInSlot == null) {
                if(direction > 0) {
                SpawnNewItem(item, slot);
                DraggedItem.count--;
                    if (DraggedItem.count <= 0) {
                            Destroy(DraggedItem.gameObject);
                    } else {
                            DraggedItem.RefreshCount();
                    }
                return true;
                } else {
                    return false;
                }
            }
        return false;
    }

    public void SpawnNewItem(Item item, InventorySlot slot) {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use) {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null) {
            Item item = itemInSlot.item;
            if (use == true) {
                itemInSlot.count--;
                if (itemInSlot.count <= 0) {
                    Destroy(itemInSlot.gameObject);
                } else {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }

        return null;
    }

    public bool StackItem(InventorySlot mainSlot) {
        InventoryItem itemInMainSlot = mainSlot.GetComponentInChildren<InventoryItem>();
        for (int i = 0; i < openSlots.Count; i++)
        {
            InventorySlot slot = openSlots[i];
            if (slot != mainSlot) {
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot != null &&
                    itemInSlot.item == itemInMainSlot.item &&
                    itemInMainSlot.count < maxStackedItems &&
                    itemInMainSlot.item.stackable == true) {
                    if ((itemInMainSlot.count + itemInSlot.count) < maxStackedItems) {
                        itemInMainSlot.count = itemInMainSlot.count + itemInSlot.count;
                        itemInSlot.count = 0;
                        if (itemInSlot.count <= 0) {
                            Destroy(itemInSlot.gameObject);
                        } else {
                            itemInSlot.RefreshCount();
                        }
                        itemInMainSlot.RefreshCount();
                    }
                    else 
                    {
                        for (int y = 0; y < itemInSlot.count; y++ ) {
                            if (itemInSlot.count < maxStackedItems) {
                             itemInMainSlot.count += 1;
                             itemInSlot.count += -1;
                             if (itemInSlot.count <= 0) {
                                Destroy(itemInSlot.gameObject);
                            } else {
                                itemInSlot.RefreshCount();
                        };
                             itemInMainSlot.RefreshCount();
                            }
                        }
                    }
                }
            }
        }
        return true;
    }
    public bool ItemsInSlotDivision(GameObject itself, Item item, InventorySlot Slot, int direction) {

        InventoryItem draggedItem = itself.GetComponent<InventoryItem>();

            ChangeItemAmountInSlot(draggedItem, item, Slot, direction*-1);
        return true;
    }
}