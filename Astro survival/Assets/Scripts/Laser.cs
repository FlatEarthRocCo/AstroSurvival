using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Animator LaserAnimator;

    // Start is called before the first frame update
    void Start()
    {
        LaserAnimator = GetComponent<Animator>();
        LaserAnimator.Play("Base Layer.Charged Animation");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.GetComponent<Rigidbody2D>())
        {
            Vector2 dir = transform.GetComponent<Rigidbody2D>().velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") == false)
        {
            if (col.CompareTag("enemy"))
            {
                if (col.GetComponent<Enemy>().healthCount > 0)
                {
                    col.GetComponent<Enemy>().healthCount -= 20;
                }
                col.GetComponent<Enemy>().TakeHit(gameObject);
            }
            Explode();
            
        }
    }
    private void Explode()
    {
        Destroy(transform.GetComponent<Rigidbody2D>()); 
        LaserAnimator.Play("Base Layer.Hit"); 
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
