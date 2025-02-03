using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float range;
    public Rigidbody2D rb2d;
    public Animator enemyAnimatons;
    private Vector3 direction;
    public int healthCount;
    public bool directionRight;
    public bool directionLeft;
    public bool directionUp;
    public bool directionDown;

    public int enemyDirection;

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

        Vector3 targetPosition = player.transform.position;
        direction = targetPosition - transform.position;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0;
            if (direction.x > 0) {
                direction.x = 1;
                    directionRight = true;
                    directionDown = false;
                    directionLeft = false;
                    directionUp = false;
                    enemyAnimatons.Play("walkR");
            }
            else{
                direction.x = -1;
                    directionRight = false;
                    directionDown = false;
                    directionLeft = true;
                    directionUp = false;
                    enemyAnimatons.Play("walkL");
            }
        }
        else
        {
            direction.x = 0;
            if (direction.y > 0) {
                direction.y = 1;
                    directionRight = false;
                    directionDown = false;
                    directionLeft = false;
                    directionUp = true;
                    enemyAnimatons.Play("walk");
            }
            else {
                direction.y = -1;
                    directionRight = false;
                    directionDown = true;
                    directionLeft = false;
                    directionUp = false;
                    enemyAnimatons.Play("walk");
            }
        }
        if(direction.y == 0 & direction.x == 0) {
                   enemyAnimatons.Play("idle");
        }

        direction.Normalize();

        rb2d.velocity = direction * speed;
        

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
