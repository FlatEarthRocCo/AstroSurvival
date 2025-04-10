using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = transform.GetComponent<Rigidbody2D>().velocity;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        print("Tiggered!");
        if (col.CompareTag("Enemy"))
        {
            if (col.GetComponent<Enemy>().healthCount >= 0)
            {
                col.GetComponent<Enemy>().healthCount -= 50;
            }
        }
        Destroy(gameObject);
    }
}
