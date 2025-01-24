using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    
    private Collider2D Collider;
    [SerializeField]
    private ContactFilter2D Filter;
    private List<Collider2D> CollidedOcjects = new List<Collider2D>(1);
    protected virtual void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HideNotNeeded();
        Collider.OverlapCollider(Filter, CollidedOcjects);
        foreach(var o in CollidedOcjects)
        {
            OnCollided(o.gameObject);
        }
    }

    protected virtual void OnCollided(GameObject collidedObject)
    {
        Debug.Log("Collided with"  + collidedObject.name);
    }

    protected virtual void HideNotNeeded()
    {}
}
