using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public InventorySlot[] craftingSlots;
    public List<Item> itemList;
    public string[] recipes;
    public Item[] recipeResult;
    public InventorySlot resultSlot;

    void CheckForRecipes() {
        
        string CurrentRecipeString = "";
        foreach(Item item in itemList){
            if(item != null){
                CurrentRecipeString += item.name;
            }else {
                CurrentRecipeString += "null";
            }
        }

        for(int i = 0; i < recipes.Length; i++)
        {
            if(recipes[i] == CurrentRecipeString) {
                if(resultSlot.GetComponentInChildren<InventoryItem>() == null){
                inventoryManager.SpawnNewItem(recipeResult[i], resultSlot);
                        for (int u = 0; u < craftingSlots.Length; u++) {
                            InventorySlot slot = craftingSlots[u];
                            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                            if(itemInSlot){
                                if(itemInSlot.count == 1){
                                    Destroy(itemInSlot.gameObject);
                                }else{
                                    itemInSlot.count -= 1;
                                    itemInSlot.RefreshCount();
                                }
                            }
                            
                        }

                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < craftingSlots.Length; i++) {
            InventorySlot slot = craftingSlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot){
            itemList[i] = itemInSlot.item;
            }else {
                itemList[i] = null;
            }
        }
        CheckForRecipes();
    }
}
