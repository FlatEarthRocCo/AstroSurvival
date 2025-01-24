using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public Transform Building;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close() {
        gameObject.transform.parent.gameObject.SetActive(false);
        Building.gameObject.GetComponent<InteractableObject>().InteractionType = false;
        Building.gameObject.GetComponent<InteractableObject>().OpenGui = false;
        Building.gameObject.GetComponent<InteractableObject>().OnInteract();
        Building.gameObject.GetComponent<InteractableObject>().InteractionType = true;
        Building.gameObject.GetComponent<InteractableObject>().OpenGui = true;
    }
}
