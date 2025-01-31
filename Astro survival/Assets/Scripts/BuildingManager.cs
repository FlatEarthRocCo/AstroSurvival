using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{   

    public GameObject buildingsFolder;
    public InventoryManager inventoryManager;
    [SerializeField] private Item usedItem;

    private Transform PlacementGhost = null;

    private bool placing = false;

    public bool OpenGui;

    public GameObject Gui;

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera WorldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, WorldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera WorldCamera)
    {
        Vector3 worldPosition = WorldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    private void Update()
    {   if(inventoryManager.GetSelectedItem(false)){ 
        Item selectedItem = inventoryManager.GetSelectedItem(false);
        if (selectedItem.type == ItemType.Building)
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            if(placing != true){
                PlacementGhost = Instantiate(usedItem.prefab, mouseWorldPosition, Quaternion.identity, buildingsFolder.transform);
                Destroy(PlacementGhost.GetComponent<BoxCollider2D>());
                placing = true;
                
            }
            if(placing == true){
                if(PlacementGhost){
                    if (CanSpawnBuilding(usedItem, mouseWorldPosition)) {
                        PlacementGhost.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material.color = Color.green;
                    } else {
                        PlacementGhost.GetChild(0).gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
                    }
                    PlacementGhost.position = mouseWorldPosition;
                }
            }
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {   
                placing = false;
                Destroy(PlacementGhost.gameObject);
                if (CanSpawnBuilding(usedItem, mouseWorldPosition)) {
                    Transform SpawnedObject = Instantiate(usedItem.prefab, mouseWorldPosition, Quaternion.identity, buildingsFolder.transform);
                    inventoryManager.GetSelectedItem(true);
                    if (OpenGui ) {
                        GameObject ObjectsGui = Instantiate(Gui, new Vector3(0,0,0), Quaternion.identity, GameObject.Find("Canvas/InvWindows").transform);
                        SpawnedObject.GetComponent<InteractableObject>().Gui = ObjectsGui;
                        ObjectsGui.transform.GetChild(1).GetComponent<CloseButton>().Building = SpawnedObject;
                    }
                }
            }
        } 
        bool num;
        if(inventoryManager.GetSelectedItem(false)) {
            num = inventoryManager.GetSelectedItem(false);
        } else {
            num = false;
        }
        print(num);
        if (!inventoryManager.GetSelectedItem(false) | selectedItem.type != ItemType.Building ) {
            if (PlacementGhost) {
                Destroy(PlacementGhost.gameObject);
                placing = false;
            }
            
        }
        } else {
            if (PlacementGhost) {
                Destroy(PlacementGhost.gameObject);
                placing = false;
            }
        }
    }

    private bool CanSpawnBuilding(Item BuildingItem, Vector3 position) {
        BoxCollider2D buildingCollider = BuildingItem.prefab.GetComponent<BoxCollider2D>();

        if (Physics2D.OverlapBox(position + (Vector3)buildingCollider.offset, buildingCollider.size, 0)  != null) {
            return false;
        } else {
            return true;
        }
    }
    
}  
    
