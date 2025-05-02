using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    public bool directionRight;
    public bool directionLeft;
    public bool directionUp;
    public bool directionDown;

    public Vector2 playerDirection;

    public GameObject faceR;
    public GameObject faceL;
    public GameObject faceU;
    public GameObject faceD;
    public GameObject faceHurt;

    public Animator playerAnimatons;

    public bool attacking;
    public float attackCoolDownTime;
    public bool justAttacked;

    public bool hurting;

    public GameObject laserPrefab;
    public float lunchForce;
    private GameObject laser;
    Quaternion rotationOfLaser;

    public int healthCount;
    public GameObject gameOverScreen;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;



    // Start is called before the first frame update
    void Start()
    {
        healthCount = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthCount > 0)
        {
            float hearts = (float)healthCount/33;
            //heart controller
            if(hearts > 2)
            {
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
            }else if (hearts > 1)
            {
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
            }
            else if (hearts > 0)
            {
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
            }
            else if (hearts <= 0)
            {
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
            }

            if(attacking == false && hurting == false)
            {
                moveInput.x = Input.GetAxisRaw("Horizontal");
                moveInput.y = Input.GetAxisRaw("Vertical");

                moveInput.Normalize();

                if(Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
                {
                    moveInput.y = 0;
                    rb2d.velocity = moveInput * moveSpeed;
                }
                else
                {
                    moveInput.x = 0;
                    rb2d.velocity = moveInput * moveSpeed;
                }

                //moving right
                if(moveInput.x > 0)
                {
                    directionRight = true;
                    directionDown = false;
                    directionLeft = false;
                    directionUp = false;
                    turnToolsOf();
                    faceR.SetActive(true);
                    faceL.SetActive(false);
                    faceD.SetActive(false);
                    faceU.SetActive(false);
                    faceHurt.SetActive(false);
                    playerAnimatons.Play("walkR");
                }

                //moving left
                if (moveInput.x < 0)
                {
                    directionRight = false;
                    directionDown = false;
                    directionLeft = true;
                    directionUp = false;
                    turnToolsOf();
                    faceR.SetActive(false);
                    faceL.SetActive(true);
                    faceD.SetActive(false);
                    faceU.SetActive(false);
                    faceHurt.SetActive(false);
                    playerAnimatons.Play("walkL");
                }

                //moving up
                if (moveInput.y > 0)
                {
                    directionRight = false;
                    directionDown = false;
                    directionLeft = false;
                    directionUp = true;
                    turnToolsOf();
                    faceR.SetActive(false);
                    faceL.SetActive(false);
                    faceD.SetActive(false);
                    faceU.SetActive(true);
                    faceHurt.SetActive(false);
                    playerAnimatons.Play("walkU");
                }
                //moving down
                if (moveInput.y < 0)
                {
                    directionRight = false;
                    directionDown = true;
                    directionLeft = false;
                    directionUp = false;
                    turnToolsOf();
                    faceR.SetActive(false);
                    faceL.SetActive(false);
                    faceD.SetActive(true);
                    faceU.SetActive(false);
                    faceHurt.SetActive(false);
                    playerAnimatons.Play("walkD");
                }

                if(moveInput.y == 0 && moveInput.x == 0)
                {
                    if (directionDown == true)
                    {
                        playerAnimatons.Play("playerIdleD");
                        playerDirection = new Vector2(0, -1);
                    }
                    else if (directionRight == true)
                    {
                        playerAnimatons.Play("playerIdleR");
                        playerDirection = new Vector2(1, 0);
                    }
                    else if (directionLeft == true)
                    {
                        playerAnimatons.Play("playerIdleL");
                        playerDirection = new Vector2(-1, 0);
                    }
                    else if (directionUp == true)
                    {
                        playerAnimatons.Play("playerIdleU");
                        playerDirection = new Vector2(0, 1);
                    }
                    
                    
                }
                if(justAttacked == true && attackCoolDownTime > 0)
                {
                    attackCoolDownTime -= Time.deltaTime;
                }

            }

            if(Input.GetKey(KeyCode.Space) && attackCoolDownTime <= 0 && hurting == false)
            {
                attacking = true;
                if (directionUp)
                {
                    playerAnimatons.Play("AttackU");
                }else if (directionDown)
                {
                    playerAnimatons.Play("AttackD");
                }
                else if (directionRight)
                {
                    playerAnimatons.Play("AttackR");
                }
                else if (directionLeft)
                {
                    playerAnimatons.Play("AttackL");
                }

                moveInput.y = 0;
                moveInput.x = 0;
                rb2d.velocity = moveInput * moveSpeed;
                attackCoolDownTime = 0.3f;
                justAttacked = true;

            }
            if(hurting == true)
            {
                faceHurt.SetActive(true);
            }

        }
        else
        {
            gameOverScreen.SetActive(true);
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
    }

    public void OnHurting(int damage)
    {
            hurting = true;
            faceHurt.SetActive(true);
            faceR.SetActive(false);
            faceL.SetActive(false);
            faceD.SetActive(false);
            faceU.SetActive(false);
            playerAnimatons.Play("playerHurt");
            healthCount = healthCount - damage;
    }

    public void turnToolsOf(){
        faceL.transform.GetChild(0).gameObject.SetActive(false);
        faceR.transform.GetChild(0).gameObject.SetActive(false);
        faceU.transform.GetChild(0).gameObject.SetActive(false);
        faceD.transform.GetChild(0).gameObject.SetActive(false);
    }
    public void turnOffAttacking() {
        attacking = false;
    }
    public void fireLaser() {
        print("FireLaser");
        laser = Instantiate(laserPrefab, transform.position, rotationOfLaser);
        laser.GetComponent<Rigidbody2D>().velocity = playerDirection * lunchForce;
    }

    public void TurnOffHurting()
    {
        hurting = false;    
    }

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }

}