using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : CollidableObject
{  
    public GameObject Gui;
    public bool OpenGui;
    public int WaitUntilNextInteraction;
    public bool Animation;
    public Animator Animator;
    public string AnimationToPlayName;
    public bool UseOnlyOnce;
    private bool OneUse = false;
    public int HowMuchToPickup;
    public SpriteRenderer spriteRenderer;
    public Sprite AfterSprite;
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;
    public GameObject interactionObject;
    public GameObject frame;
    private bool Interacted = false;
    public bool InteractionType = true;

    protected override void OnCollided(GameObject collidedObject)
    {   
        if(UseOnlyOnce){
        if (!OneUse)
        {
            frame.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                OnInteract();

            }
        }
        } else {
            frame.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                OnInteract();

            }
        }
    }

    protected override void HideNotNeeded()
    {
        frame.SetActive(false);
    }

    public virtual void OnInteract() 
    {
        if (!Interacted)
        {
            print("Interacted!!!");
            Interacted = true;
            OneUse = true;
            if(!Animation){
            spriteRenderer.sprite = AfterSprite;
            } else {
                Animator.SetBool(AnimationToPlayName, InteractionType);
            }
            for(int i=0; i<1; i++){
                PickupItem(0);
            }
            if(OpenGui){
                Gui.SetActive(true);
            }
            StartCoroutine(waitForNextInteraction());
        }
        
    }

    public void PickupItem(int id) {

        for (int i = 0; i < HowMuchToPickup; i++) 
        {
            bool result = inventoryManager.AddItem(itemsToPickup[id]);
        }  
    }

    IEnumerator waitForNextInteraction() {
        yield return new WaitForSeconds(WaitUntilNextInteraction);
        Interacted = false;
    }
}
