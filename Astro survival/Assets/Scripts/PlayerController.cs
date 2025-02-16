using System.Collections;
using System.Collections.Generic;
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

    public int playerDirection;

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

    public GameObject swordDown;
    public GameObject swordUp;
    public GameObject swordRight;
    public GameObject swordLeft;
    public GameObject bowDown;
    public GameObject bowUp;
    public GameObject bowRight;
    public GameObject bowLeft;
    public int weaponInUse;

    public GameObject arrowPrefab;

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
            //weapons in use

            if(weaponInUse == 0)
            {
                swordDown.SetActive(true);
                swordLeft.SetActive(true);
                swordRight.SetActive(true);
                swordUp.SetActive(true);
                bowDown.SetActive(false);
                bowLeft.SetActive(false);
                bowRight.SetActive(false);
                bowUp.SetActive(false);
            }else if(weaponInUse == 1)
            {
                swordDown.SetActive(false);
                swordLeft.SetActive(false);
                swordRight.SetActive(false);
                swordUp.SetActive(false);
                bowDown.SetActive(true);
                bowLeft.SetActive(true);
                bowRight.SetActive(true);
                bowUp.SetActive(true);
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                weaponInUse = 0;
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                weaponInUse = 1;
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
                    }
                    else if (directionRight == true)
                    {
                        playerAnimatons.Play("playerIdleR");
                    }
                    else if (directionLeft == true)
                    {
                        playerAnimatons.Play("playerIdleL");
                    }
                    else if (directionUp == true)
                    {
                        playerAnimatons.Play("playerIdleU");
                    }
                    
                    
                }
                if(justAttacked == true && attackCoolDownTime > 0)
                {
                    attackCoolDownTime -= Time.deltaTime;
                }

            }

            if(Input.GetKey(KeyCode.Space) && attackCoolDownTime <= 0 && hurting == false)
            {
                if (directionUp)
                {
                    playerAnimatons.Play("attackU");
                }else if (directionDown)
                {
                    playerAnimatons.Play("attackD");
                }
                else if (directionRight)
                {
                    playerAnimatons.Play("attackR");
                }
                else if (directionLeft)
                {
                    playerAnimatons.Play("attackL");
                }

                if(weaponInUse == 1)
                {
                    if (directionUp)
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                    }
                    if (directionDown)
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 270)));
                    }
                    if (directionRight)
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                    }
                    if (directionLeft)
                    {
                        Instantiate(arrowPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                    }
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

    public void restartGame()
    {
        SceneManager.LoadScene(0);
    }

}