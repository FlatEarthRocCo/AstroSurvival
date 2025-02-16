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
    public bool justAttacked = false;
    public Collider2D collider;

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
        if (enemyAnimatons.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            justAttacked = false;
        }
        
        Vector3 Offset = transform.position - player.transform.position;
        float Distance = Offset.magnitude;
        if(Distance <= range) {
            Swarm();
        }else{
            rb2d.velocity = Vector2.zero;
            enemyAnimatons.Play("idle");
        }

    }

    private void Attack(Collider2D col) {
        if(!justAttacked) {
            justAttacked = true;
            collider = col;
            collider.GetComponent<playerController>().healthCount -= damage;
            if(directionLeft == false){
                enemyAnimatons.Play("attackHorizontalR");
            }else{
                enemyAnimatons.Play("attackHorizontalL");   
            }
            
        }
    }

    private void Swarm()
    {
        if(!justAttacked) {
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
                    directionDown = false;
                    directionUp = true;
                    if(directionLeft == false){
                        enemyAnimatons.Play("walkR");
                    }else{
                        enemyAnimatons.Play("walkL");
                    }
            }
            else {
                direction.y = -1;
                    directionDown = true;
                    directionUp = false;
                    if(directionLeft == false){
                        enemyAnimatons.Play("walkR");
                    }else{
                        enemyAnimatons.Play("walkL");
                    }
            }
        }
        if(direction.y == 0 & direction.x == 0) {
                   enemyAnimatons.Play("idle");
        }

        direction.Normalize();

        rb2d.velocity = direction * speed;
        } else {
            rb2d.velocity = Vector2.zero;
        }
        

    }

    private void TurnOffAttack(){
        player.GetComponent<playerController>().hurting = false;
        print("TurnOffAttck");
    }

    private void HurtPlayer() {
        player.GetComponent<playerController>().OnHurting(damage);
    }

    private void  OnTriggerStay2D(Collider2D col) 
    {
        if(col.CompareTag("Player"))
        {   
            if(col.GetComponent<playerController>().healthCount >= 0) 
            {
                Attack(col);
            }
        }
    }
}
