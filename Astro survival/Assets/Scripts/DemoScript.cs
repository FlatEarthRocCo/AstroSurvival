using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id) {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
    }

    public void GetSelectedItem() {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        if (receivedItem != null) {
            Debug.Log("Received item:" + receivedItem);
        }
    }

        public void UseSelectedItem() {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        if (receivedItem != null) {
            Debug.Log("Used item:" + receivedItem);
        }
    }

        public void StackSelectedItem() {
        //    bool result = inventoryManager.StackItem();
        }
}
