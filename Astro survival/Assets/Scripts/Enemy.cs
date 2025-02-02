using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float range;
    private Vector3 direction;

    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private EnemyData data;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Offset = transform.position - player.transform.position;
        float Distance = Offset.magnitude;
        if(Distance <= range) {
            Swarm();
        }

    }

    private void Swarm()
    {
        if (player == null)
            return;

        Vector3 targetPosition = player.transform.position;
        direction = targetPosition - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0;
            if (direction.x > 0)
                direction.x = 1;
            else
                direction.x = -1;
        }
        else
        {
            direction.x = 0;
            if (direction.y > 0)
                direction.y = 1;
            else
                direction.y = -1;
        }

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
        
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);    
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.CompareTag("Player"))
        {
            if(collider.GetComponent<playerController>().healthCount != null) 
            {
                collider.GetComponent<playerController>().healthCount -= damage;
            }
        }
    }
}
