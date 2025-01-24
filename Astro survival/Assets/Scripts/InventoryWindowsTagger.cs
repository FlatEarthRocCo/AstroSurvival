using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindowsTagger : MonoBehaviour
{   

    public InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<InventorySlot> openSlots = inventoryManager.openSlots;
        foreach(Transform child in transform)
        {
            GameObject InvWindow = child.gameObject;
            bool IsActive = InvWindow.activeSelf;
            foreach(InventorySlot Slot in InvWindow.GetComponentsInChildren<InventorySlot>())
            {
                if (IsActive) {
                    Slot.gameObject.tag = "Open";
                } else {
                    Slot.gameObject.tag = "Closed";
                    if (openSlots.Contains(Slot)) {
                        openSlots.Remove(Slot);
                    }
                }
            }
        }
    }
}

